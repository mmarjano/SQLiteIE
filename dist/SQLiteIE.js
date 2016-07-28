if (!window.sqlitePlugin && !window.openDatabase) { // websql is not supported and no cordova SQLite plugin is loaded
    try {
        var plugin = new ActiveXObject("SQLiteIE.SQLiteControl");

        if (plugin !== null) {
            window.openDatabase = (function (plugin) {
                var extractObject = function (propertyNames, item) {
                    var ret = {};
                    for (var j = 0; j < propertyNames.length; j++) {
                        ret[propertyNames[j]] = item[propertyNames[j]];
                    }

                    return ret;
                };

                var transaction = ((function () {
                    function transaction(axTransaction) {
                        this._axTransaction = axTransaction;
                    }

                    transaction.prototype.executeSql = function (sqlStatement, arguments, callback, errorCallback) {
                        var axCallback = function (tx, resultSet) {
                            if (callback) {
                                debugger;
                                var tran = new transaction(tx);

                                var propertyNames = resultSet.RowList.GetPropertyNames();
                                var data = [];

                                for (var i = 0; i < resultSet.RowList.Length; i++) {
                                    var item = resultSet.RowList.Item(i);
                                    var targetItem = extractObject(propertyNames, item);
                                    
                                    data.push(targetItem);
                                }

                                var it = {
                                    rows: {
                                        item: function (index) {
                                            return data[index]
                                        },
                                        length: data.length
                                    }
                                };

                                callback(tran, it);
                            }
                        }

                        var axErrorCallback = function (tx, error) {
                            if (errorCallback) {
                                var tran = new transaction(tx);
                                errorCallback(tran, error);
                            }
                        }

                        this._axTransaction.ExecuteSql(sqlStatement, arguments, axCallback, axErrorCallback);
                    }

                    return transaction;
                })());

                var database = ((function () {
                    function database(axDatabase) {
                        this._axDatabase = axDatabase;
                    }

                    database.prototype.transaction = function (callback, errorCallback, successCallback) {

                        // Wrap JS callbacks into custom function, so that we can modify values sent back from ActiveX control
                        var axCallback = function (tx) {
                            var tran = new transaction(tx);
                            callback(tran);
                        };

                        var axErrorCallback = function (tx, error) {
                            if (errorCallback) {
                                var tran = new transaction(tx);
                                errorCallback(tran, error);
                            }
                        };

                        var axSuccessCallback = function (tx, data) {
                            if (successCallback) {
                                var tran = new transaction(tx);
                                successCallback(tran, data);
                            }
                        };

                        this._axDatabase.Transaction(axCallback, axErrorCallback, axSuccessCallback);
                    }

                    return database;
                })());

                

                return function Database(name, version, displayName, estimatedSize, creationCallback, databaseLocationIE) {
                    // Input parameters sanity check
                    if (typeof name === "undefined" || name === null || typeof databaseLocationIE === "undefined" || databaseLocationIE === null) {
                        throw new Error("Can't open database. Parameters name and databaseLocationIE are mandatory!")
                    }

                    var axDatabase = plugin.OpenDatabase({ name: name, databaseLocation: databaseLocationIE });

                    return new database(axDatabase);
                }
            })(plugin);
        }
        else {
            throw new Error("Failed to initialize SQLite plugin.");
        }
    }
    catch (e) {
        debugger;
        // an error has occured, plugin wasn't initialized successfully
    }
}