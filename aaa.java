namespace Metrics
{

import java.io.BufferedReader;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.util.Date;
import java.util.Iterator;
import java.util.Set;
class test {
}
return true;
}
/**
* @param name
* @return
@@ -103,4 +109,69 @@*/
BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
return br.readLine();
}

 static public int askFor(String msg, String answer[]) throws IOException {
 // No MSG and no answer selection
 if (msg == null || answer == null || answer.length == 0)
 return -1;

 while (true) {
 System.out.print(msg);
 System.out.flush();

 String response = SepUtil.pause();

 //Is the response in the answer selection?
 for (int i = 0; i < answer.length; i++) {
 if (response.equalsIgnoreCase(answer[i]))
 return i;
 }
 }
 }

 /**
 * Load all methods from given File
 * 
 * @param in
 * @return
 * @throws Exception
 */
 public static Object loadMethods(File in) throws Exception {
 if (!in.exists())
 return null;

 ObjectInputStream oin = new ObjectInputStream(new FileInputStream(in));
 Object obj = oin.readObject();
 oin.close();
 return obj;
 }

 /**
 * Store methods
 * 
 * @param methods
 * @param out
 * @throws Exception
 */
 public static void storeMethods(Object obj, File out, boolean overwrite)
 throws Exception {

 if (out.exists()) {
 if (overwrite) {
 out.delete();
 } else {
 return;
 }
 }

 ObjectOutputStream oout = new ObjectOutputStream(new FileOutputStream(
 out));
 oout.writeObject(obj);
 oout.close();
 }

 public static void showInfo(String info) {
 System.out.println(new Date() + "-" + info);

 }
}


Modified: trunk/src/edu/ucsc/cse/grase/sep/anal/Analysis.java
===================================================================
--- trunk/src/edu/ucsc/cse/grase/sep/anal/Analysis.java 2005-02-12 17:00:04 UTC (rev 87)
+++ trunk/src/edu/ucsc/cse/grase/sep/anal/Analysis.java 2005-02-12 19:22:40 UTC (rev 88)
@@ -108,7 +108,18 @@
Method m1 = (Method) obj1;
Method m2 = (Method) obj2;

- return m2.getSignatures().size() - m1.getSignatures().size();
+ if (m2.getSignatures().size() != m1.getSignatures().size())
+ return m2.getSignatures().size()
+ - m1.getSignatures().size();
+ 
+ // Check method name
+ String name1 = m1.getName();
+ String name2 = m2.getName();
+ if (name1==null || name2==null)
+ return 0;
+ 
+ // String compare
+ return name1.compareTo(name2);
}
});

@@ -147,8 +158,6 @@
}
out.println();
}
- 
- 

private String toArrayString(List list) {
String ret = "";
@@ -199,7 +208,7 @@
// No old sig, no annotation
if (oldSig == null)
return "";
- 
+
if (newSig == null) {
logger.error("New Sig cannot be null");
return "";
@@ -208,7 +217,7 @@
// Reset parameter matches
oldSig.resetParamMatch();
newSig.resetParamMatch();
- 
+
String annotation = "";

List oldParameters = oldSig.getParameters();

Modified: trunk/src/edu/ucsc/cse/grase/sep/anal/Parameter.java
===================================================================
--- trunk/src/edu/ucsc/cse/grase/sep/anal/Parameter.java 2005-02-12 17:00:04 UTC (rev 87)
+++ trunk/src/edu/ucsc/cse/grase/sep/anal/Parameter.java 2005-02-12 19:22:40 UTC (rev 88)
@@ -3,6 +3,7 @@
*/
package edu.ucsc.cse.grase.sep.anal;

+import java.io.Serializable;
import java.util.ArrayList;
import java.util.StringTokenizer;

@@ -14,7 +15,7 @@
* TODO To change the template for this generated type comment go to Window -
* Preferences - Java - Code Generation - Code and Comments
*/
-public class Parameter {
+public class Parameter implements Serializable {
boolean isReturn;

String modifier = "";

Modified: trunk/src/edu/ucsc/cse/grase/sep/anal/Sep.java
===================================================================
--- trunk/src/edu/ucsc/cse/grase/sep/anal/Sep.java 2005-02-12 17:00:04 UTC (rev 87)
+++ trunk/src/edu/ucsc/cse/grase/sep/anal/Sep.java 2005-02-12 19:22:40 UTC (rev 88)
@@ -6,44 +6,55 @@
*/
package edu.ucsc.cse.grase.sep.anal;

-import java.io.BufferedReader;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
-import java.io.InputStreamReader;
import java.io.PrintStream;
-
import java.util.Properties;

+import org.apache.log4j.Logger;
+
+import edu.ucsc.cse.grase.sep.SepUtil;
+
/**
* @author Sung Kim
* 
* The main class for SEP It reads configuration file and run Main or MainLocal
*/
public class Sep {
+ static Logger logger = Logger.getLogger(Sep.class);

public static void main(String[] args) throws Exception {
String revision = "$Rev$";
System.out.println(revision);

if (args.length != 1) {
- System.out
+ System.err
.println("Kenyon analyser requires a processing-run configuration file");
return;
- } else {
- File configfp = new File(args[0]);
- if (!configfp.exists()) {
- System.out.println("The specified configuration file "
- + args[0] + " does not exist.");
- return;
- } else if (!configfp.canRead()) {
- System.out
- .println("Kenyon cannot read the specified configuration file "
- + args[0]);
- } else {
- configureAndRun(configfp);
- }
}
+
+ // TODO: Want to delete log before we start the SEP
+ // FIXME: Temp solution
+ File log = new File("/tmp/sep.log");
+ if (log.exists())
+ log.delete();
+
+ File configfp = new File(args[0]);
+ if (!configfp.exists()) {
+ System.err.println("The specified configuration file " + args[0]
+ + " does not exist.");
+ return;
+ }
+
+ if (!configfp.canRead()) {
+ System.err
+ .println("Kenyon cannot read the specified configuration file "
+ + args[0]);
+ return;
+ }
+ // read configuration and run
+ configureAndRun(configfp);
}

public static void configureAndRun(File configfp) throws Exception {
@@ -57,41 +68,68 @@
if (project == null || xmlOut == null)
throw new Exception("Cannot find project id or xml_out");

- System.out.println("Running Sep for " + project + " with " + xmlOut
+ SepUtil.showInfo("Running Sep for " + project + " with " + xmlOut
+ " dir.");

File annOut = new File(xmlOut, project + ".ann");
if (annOut.exists()) {
- BufferedReader in = new BufferedReader(new InputStreamReader(
- System.in));
+ String msg = "Annotation file exist! Do you want to rebuild the file? [Yes/No]";
+ int response = SepUtil.askFor(msg, new String[] { "Yes", "No" });

- while (true) {
- System.out
- .println("Annotation file exist! Do you want to rebuild the file? [Y/N]");
-
- String response = in.readLine();
- if (response.startsWith("N")) {
- finalReport(annOut);
- return;
- } else if (response.startsWith("Y")) {
- break;
- }
+ if (response == 1) {
+ finalReport(annOut);
+ return;
}
}

- System.out.println("Reading graphs ...");
- // All methods holder 
+ File methodsOut = new File(xmlOut, project + ".methods");
+ boolean useMethodOut = false;
+ if (methodsOut.exists()) {
+ String msg = "Methods file exist! Do you want to rebuild the file? [Yes/No]";
+ int response = SepUtil.askFor(msg, new String[] { "Yes", "No" });
+
+ // NO
+ if (response == 1)
+ useMethodOut = true;
+ }
+
+ // All methods holder
AllMethods methods = null;
- if (useDB.equalsIgnoreCase(("true"))) {
- methods = FillMethods.fillMethodsFromDB(project);
- } else {
- methods = FillMethods.fillMethodsFromLocal(xmlOut + "/index.xml");
+
+ if (useMethodOut) {
+ try {
+ SepUtil.showInfo("Reading ALl methods ...");
+ // Read All methods from file OBJ
+ methods = (AllMethods) SepUtil.loadMethods(methodsOut);
+ SepUtil.showInfo("Reading ALl methods done.");
+ } catch (Exception e) {
+ e.printStackTrace();
+ // Let's rebuild graph
+ useMethodOut = false;
+ }
}

- System.out.println("Automatic Annotating ...");
+ if (!useMethodOut) {
+ SepUtil.showInfo("Reading graphs ...");

+ if (useDB.equalsIgnoreCase(("true"))) {
+ methods = FillMethods.fillMethodsFromDB(project);
+ } else {
+ methods = FillMethods.fillMethodsFromLocal(xmlOut
+ + "/index.xml");
+ }
+
+ SepUtil.showInfo("Reading graphs done.");
+
+ SepUtil.showInfo("Writing ALl methods ...");
+ // Let's save methods to obj File
+ SepUtil.storeMethods(methods, methodsOut, true);
+ }
+
+ // Run automatic annotation
+ SepUtil.showInfo("Automatic Annotating ...");
new Analysis().run(methods, annOut);
-
+ SepUtil.showInfo("Automatic Annotating done");
askFianlReport(annOut);
}

@@ -111,22 +149,15 @@
* @param annOut
*/
private static void askFianlReport(File annOut) throws Exception {
- BufferedReader in = new BufferedReader(new InputStreamReader(System.in));
- while (true) {
- System.out.println("Automatic Annotation is done.");
- System.out.println("Check the annotation file: "
- + annOut.getAbsolutePath()
- + " and type 'OK' to proceed or 'EXIT' to finish: ");
+ String msg = "Automatic Annotation is done.
";
+ msg += "Check the annotation file: " + annOut.getAbsolutePath()
+ + " and type 'OK' to proceed or 'EXIT' to finish: ";
+ int response = SepUtil.askFor(msg, new String[] { "EXIT", "OK" });

- String response = in.readLine();
+ if (response == 1)
+ finalReport(annOut);

- if (response.equalsIgnoreCase("Exit")) {
- return;
- } else if (response.equalsIgnoreCase("ok")) {
- finalReport(annOut);
- return;
- }
- }
+ return;
+ }
+}
}
- }
-}
}