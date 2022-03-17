

$('#btnSelectData').click(function () {
    btnSelectData_click();
});

$('#btnCopyTo').click(function () {
    btnCopyTo_click();
});

$("input[name=jsonMode]").click(function () {
    drawData(savedData);
});

var savedData;

function drawData(allData) {

    var mode = $("input[name=jsonMode]:checked").val();

    if (mode == 'Object') {
        allData = tableToJsonRows(allData);
    }


    var prettyJson = syntaxHighlight(JSON.stringify(allData, undefined).replace(/],/g, '],\r\n'));

    $('#result').height($("#visualizationContainer").height() - 150);
    $('#result').width($("#visualizationContainer").width());
    $('#result').html(prettyJson);


}

function tableToJsonRows(table) {

    var jsonRows = [];


    var cols = table[0].length;
    var array = table;

    for (var i = 1; i < table.length; i++) {
        var rowJson = {}
        var row = array[i];
        for (var j = 0; j < cols; j++) {
            var col = table[0][j];
            rowJson[col] = row[j];
        }
        jsonRows.push(rowJson);
    }



    return jsonRows;
}

function syntaxHighlight(json) {
    json = json.replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;');
    return json.replace(/("(\\u[a-zA-Z0-9]{4}|\\[^u]|[^\\"])*"(\s*:)?|\b(true|false|null)\b|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?)/g, function (match) {
        var cls = 'number';
        if (/^"/.test(match)) {
            if (/:$/.test(match)) {
                cls = 'key';
            } else {
                cls = 'string';
            }
        } else if (/true|false/.test(match)) {
            cls = 'boolean';
        } else if (/null/.test(match)) {
            cls = 'null';
        }
        return '<span class="' + cls + '">' + match + '</span>';
    });
}

function btnSelectData_click() {
    Office.context.document.bindings.addFromPromptAsync(Office.BindingType.Matrix
          , { id: 'myData', promptText: 'Please select a range' }
          , function (asyncResult) {
              if (asyncResult.error) {
                  return;
              }
              var binding = asyncResult.value;
              setupBinding(binding);
          });
}

function btnCopyTo_click() {
    $('#result').select();
    clipboardData.setData("Text", $('#result').text());

}

function setupBinding(binding) {
    binding.addHandlerAsync(
                     Office.EventType.BindingDataChanged,
                     onBindingDataChangedHandler
                 );
    refreshData(binding);
}

function onBindingDataChangedHandler(changedArgs) {
    refreshData(changedArgs.binding);
}

function refreshData(binding) {
    binding.getDataAsync({ filterType: Office.FilterType.OnlyVisible, coercionType: Office.CoercionType.Matrix },
                      function (result) {
                          if (result.status == 'failed') {
                              $('#result').html('Error: ' + result.error.message);
                              return;
                          }
                          var allData = result.value;

                          savedData = allData;
                          drawData(allData);
                      });
}

$('#btnSelectData').click(function () {
    btnSelectData_click();
});

$('#btnCopyTo').click(function () {
    btnCopyTo_click();
});

$("input[name=jsonMode]").click(function () {
    drawData(savedData);
});

var savedData;

function drawData(allData) {

    var mode = $("input[name=jsonMode]:checked").val();

    if (mode == 'Object') {
        allData = tableToJsonRows(allData);
    }


    var prettyJson = syntaxHighlight(JSON.stringify(allData, undefined).replace(/],/g, '],\r\n'));

    $('#result').height($("#visualizationContainer").height() - 150);
    $('#result').width($("#visualizationContainer").width());
    $('#result').html(prettyJson);


}

function tableToJsonRows(table) {

    var jsonRows = [];


    var cols = table[0].length;
    var array = table;

    for (var i = 1; i < table.length; i++) {
        var rowJson = {}
        var row = array[i];
        for (var j = 0; j < cols; j++) {
            var col = table[0][j];
            rowJson[col] = row[j];
        }
        jsonRows.push(rowJson);
    }



    return jsonRows;
}

function syntaxHighlight(json) {
    json = json.replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;');
    return json.replace(/("(\\u[a-zA-Z0-9]{4}|\\[^u]|[^\\"])*"(\s*:)?|\b(true|false|null)\b|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?)/g, function (match) {
        var cls = 'number';
        if (/^"/.test(match)) {
            if (/:$/.test(match)) {
                cls = 'key';
            } else {
                cls = 'string';
            }
        } else if (/true|false/.test(match)) {
            cls = 'boolean';
        } else if (/null/.test(match)) {
            cls = 'null';
        }
        return '<span class="' + cls + '">' + match + '</span>';
    });
}

function btnSelectData_click() {
    Office.context.document.bindings.addFromPromptAsync(Office.BindingType.Matrix
          , { id: 'myData', promptText: 'Please select a range' }
          , function (asyncResult) {
              if (asyncResult.error) {
                  return;
              }
              var binding = asyncResult.value;
              setupBinding(binding);
          });
}

function btnCopyTo_click() {
    $('#result').select();
    clipboardData.setData("Text", $('#result').text());

}

function setupBinding(binding) {
    binding.addHandlerAsync(
                     Office.EventType.BindingDataChanged,
                     onBindingDataChangedHandler
                 );
    refreshData(binding);
}

function onBindingDataChangedHandler(changedArgs) {
    refreshData(changedArgs.binding);
}

function refreshData(binding) {
    binding.getDataAsync({ filterType: Office.FilterType.OnlyVisible, coercionType: Office.CoercionType.Matrix },
                      function (result) {
                          if (result.status == 'failed') {
                              $('#result').html('Error: ' + result.error.message);
                              return;
                          }
                          var allData = result.value;

                          savedData = allData;
                          drawData(allData);
                      });
}

$('#btnSelectData').click(function () {
    btnSelectData_click();
});

$('#btnCopyTo').click(function () {
    btnCopyTo_click();
});

$("input[name=jsonMode]").click(function () {
    drawData(savedData);
});

var savedData;

function drawData(allData) {

    var mode = $("input[name=jsonMode]:checked").val();

    if (mode == 'Object') {
        allData = tableToJsonRows(allData);
    }


    var prettyJson = syntaxHighlight(JSON.stringify(allData, undefined).replace(/],/g, '],\r\n'));

    $('#result').height($("#visualizationContainer").height() - 150);
    $('#result').width($("#visualizationContainer").width());
    $('#result').html(prettyJson);


}

function tableToJsonRows(table) {

    var jsonRows = [];


    var cols = table[0].length;
    var array = table;

    for (var i = 1; i < table.length; i++) {
        var rowJson = {};
        var row = array[i];
        for (var j = 0; j < cols; j++) {
            var col = table[0][j];
            rowJson[col] = row[j];
        }
        jsonRows.push(rowJson);
    }



    return jsonRows;
}

function syntaxHighlight(json) {
    json = json.replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;');
    return json.replace(/("(\\u[a-zA-Z0-9]{4}|\\[^u]|[^\\"])*"(\s*:)?|\b(true|false|null)\b|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?)/g, function (match) {
        var cls = 'number';
        if (/^"/.test(match)) {
            if (/:$/.test(match)) {
                cls = 'key';
            } else {
                cls = 'string';
            }
        } else if (/true|false/.test(match)) {
            cls = 'boolean';
        } else if (/null/.test(match)) {
            cls = 'null';
        }
        return '<span class="' + cls + '">' + match + '</span>';
    });
}

function btnSelectData_click() {
    Office.context.document.bindings.addFromPromptAsync(Office.BindingType.Matrix
          , { id: 'myData', promptText: 'Please select a range' }
          , function (asyncResult) {
              if (asyncResult.error) {
                  return;
              }
              var binding = asyncResult.value;
              setupBinding(binding);
          });
}

function btnCopyTo_click() {
    $('#result').select();
    clipboardData.setData("Text", $('#result').text());

}

function setupBinding(binding) {
    binding.addHandlerAsync(
                     Office.EventType.BindingDataChanged,
                     onBindingDataChangedHandler
                 );
    refreshData(binding);
}

function onBindingDataChangedHandler(changedArgs) {
    refreshData(changedArgs.binding);
}

function refreshData(binding) {
    binding.getDataAsync({ filterType: Office.FilterType.OnlyVisible, coercionType: Office.CoercionType.Matrix },
                      function (result) {
                          if (result.status == 'failed') {
                              $('#result').html('Error: ' + result.error.message);
                              return;
                          };
                          var allData = result.value;

                          savedData = allData;
                          drawData(allData);
                      });
}
