var home = function () {
    "use strict";
    return {}
}(),
    codeMirrorHTML, codeMirrorJS, codeMirrorCss;
(function () {
    if (typeof (Office) == "undefined") {
        init();
    } else {
        Office.initialize = function (reason) {
            init();
        };
    }

})();


function init() {
    try {
        $(document).ready(function () {
            app.initialize();
            initCodeMirror();
            $("#tab_html").click(function (n) {
                n.preventDefault();
                $("#tab_html").tab("show");
                $("#tab_html").blur();
                codeMirrorHTML.getValue() === "" && (codeMirrorHTML.setValue(" "), codeMirrorHTML.setValue(""));
                codeMirrorHTML.setValue(codeMirrorHTML.getValue() + "");
            });
            $("#tab_js").click(function (n) {
                n.preventDefault();
                $("#tab_js").tab("show");
                $("#tab_js").blur();
                codeMirrorJS.getValue() === "" && (codeMirrorJS.setValue(" "), codeMirrorJS.setValue(""));
                codeMirrorJS.setValue(codeMirrorJS.getValue() + "");
            });
            $("#tab_css").click(function (n) {
                n.preventDefault();
                $("#tab_css").tab("show");
                $("#tab_css").blur();
                codeMirrorCss.getValue() === "" && (codeMirrorCss.setValue(" "), codeMirrorCss.setValue(""));
                codeMirrorCss.setValue(codeMirrorCss.getValue() + "");
            });
            $("#tab_samples").click(function (n) {
                n.preventDefault();
                $("#tab_samples").tab("show");
                $("#tab_samples").blur();
            });

            $('#cmdRun').click(function (n) {
                runCodeMirror();
            });

            $('#cmdShare').click(function (n) {
                runCodeMirror();
            });

            $('#cmdFinishSave').click(function (n) {
                $('#saveModal').modal('hide');
                saveFiddle();
            });

            $('#cmdSearch').click(function (n) {
            search();
            });


        });
    } catch (e) {
        app.displayError("Script error", "Error running script:<br/> " + e.message);
    }

}

function runCodeMirror() {
    //if (!window.ViewOnly){
    try {
        var textHtml = codeMirrorHTML.getValue() + "<style type='text/css'>" + codeMirrorCss.getValue() + "</style>",
        textJs = codeMirrorJS.getValue();
        if (textHtml !== "" || textJs !== "") {
            $("#editor").slideUp(200, function () {
                $("#settings").slideDown(200, function () {
                    $("#tab_html").tab("show");
                });
            });

            $('#tab_preview').tab("show");
            $('#previewcontent').html(textHtml);
            try {
                eval(textJs);
            } catch (err) {
                app.displayError("Script error", "Error running script:<br/> " + err.message);
            }
            saveToOffice();
        }
    } catch (e) {
        app.displayError("Script error", "Error running code mirror:<br/> " + e.message);
    }
    //}
}
function initCodeMirror() {
    codeMirrorHTML = CodeMirror.fromTextArea(document.getElementById("html_code_id"), {
        mode: "xml",
        htmlMode: !0,
        lineNumbers: !0
    });
    codeMirrorHTML.setSize(null, 300);
    codeMirrorJS = CodeMirror.fromTextArea(document.getElementById("js_code_id"), {
        mode: "javascript",
        lineNumbers: !0,
        matchBrackets: !0
    });
    codeMirrorJS.setSize(null, 300);

    codeMirrorCss = CodeMirror.fromTextArea(document.getElementById("css_code_id"), {
        mode: "css",
        lineNumbers: !0,
        matchBrackets: !0
    });
    codeMirrorCss.setSize(null, 300);

    var tempJs;
    codeMirrorHTML.setValue("");
    codeMirrorJS.setValue("");
    codeMirrorCss.setValue("");
    var data = $('#previewcontent').html().trim();
   if(data){
    data = data.split("</script>");
    var tempHtml = data[1].split("</style>");
    var temphtmlcss = tempHtml[0];
    var finalhtml = tempHtml[1];
    codeMirrorHTML.setValue(finalhtml);
    codeMirrorCss.setValue(temphtmlcss.replace("</style>", "").replace("<style>", ""));
 

    tempJs = data[0].replace("</script>", "").replace("<script>", "");
    codeMirrorJS.setValue(tempJs);

    runCodeMirror();
   }
}
function saveToOffice() {
    if (Office && Office.context && Office.context.document) {
        var n = codeMirrorHTML.getValue(),
            t = codeMirrorJS.getValue(),
            i = new Date,
            r = {
                htmlCode: n,
                jsCode: t,
                date: i.toUTCString(),
                appVersion: app.appVersion
            };
        Office.context.document.settings.set("savedSettings", r);
        Office.context.document.settings.saveAsync(function () { });
    }
}
function saveFiddle() {
    try {


        var data = {
            CSS: escape(codeMirrorCss.getValue()),
            HTML: escape(codeMirrorHTML.getValue()),
            JS: escape(codeMirrorJS.getValue()),
            Name: $('#txtName').val(),
            Description: $('#txtDesc').val(),
            id: $('#fiddleID').val(),
            IsPublic: $('#isPublic').prop('checked'),
            CatID: $('#selCategory').val()
        };

        $.ajax({
            url: "/user/savefiddle",
            data: data,
            type: "POST"
        }).done(function (newid) {
            $("#fiddleID").val(newid);
        });


    } catch (e) {
        app.displayError(e.message);
    }

}

function search() {
    var value = $('#txtSearch').val();
    if(value){
    if (value.isNumeric) {
        location.href = "/app?id=" + value;
    } else {
        location.href = "/app/find?searchterm=" + value;
    }
    }
}

