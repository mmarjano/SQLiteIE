# SQLiteIE
An ActiveX plugin for Internet Explorer that adds support for SQLite and mimics WebSQL API.

# Goal
As a developer, I deal alot with Cordova applications. For offline storage in lager applications usually an SQLite database is used that is provided through a Cordova plugin. 
It's all great, but how do I build and debug an app in my IDE (which happens to be Visual Studio) on PC? Chrome (still) supports WebSQL which is great and enables such scenario, 
but Visual Studio can't easily connect to Chrome for debugging and very often you're required to use Chrome's built-in developer tools. But I ideally want to use my IDE for debugging
because I'm more familiar with it and more productive using it.
Since Visual Studio supports JavaScript debugging in Internet Explorer, I built this plugin to bring WebSQL/SQLite support to IE. This enables me to do all my work using Visual Studio only.

# How to use
1. Compile the project
2. Register the dll
   ```
   regasm SQLiteIE.dll /codebase /tlb
   ```
3. Reference SQLiteIE.js in your .html file
   ```
   <script type="text/javascript" src="scripts/SqliteIE.js"></script>
   ```
4. Modify openDatabase call to include databaseLocationIE parameter
	```javascript
    // parameters version, displayName, estimatedSize, creationCallback are ignored...
	var db = window.openDatabase('myDBName', null, null, null, null, "C:\\mySQLiteDB\\");
	```
5. Execute queries as you would with normal WebSQL

# Future work
1. Implement more of WebSQL to be plug&play compatible with existing applications that use WebSQL
2. Implement basic profiler to help identify problems with queries