function isAlphaNumeric(e) { // Alphanumeric only
    var k;
    document.all ? k = e.keycode : k = e.which;
    return ((k > 47 && k < 58) || (k > 64 && k < 91) || (k > 96 && k < 123) || k == 0);
}

function isIntegerNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;

    return true;
}


function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46)
        return false;

    return true;
}

//For Decimal
function isDecimalNumberKey(evt) {
    var decFlag = false;
    var charCode = (evt.which) ? evt.which : event.keyCode


    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46)
        return false;


    return true;
}

function fnValidatePAN(Obj) {
    if (Obj == null) Obj = window.event.srcElement;
    if (Obj.value != "") {
        ObjVal = Obj.value;
        var panPat = /^([a-zA-Z]{5})(\d{4})([a-zA-Z]{1})$/;
        var code = /([C,P,H,F,A,T,B,L,J,G])/;
        var code_chk = ObjVal.substring(3, 4);
        if (ObjVal.search(panPat) == -1) {
            alert("Invalid Pan No");
            Obj.focus();
            return false;
        }
        if (code.test(code_chk) == false) {
            alert("Invaild PAN Card No.");
            return false;
        }
    }
}



function moneyFormat(textObj) {
    var newValue = textObj.value;
    var decAmount = "";
    var dolAmount = "";
    var decFlag = false;
    var aChar = "";

    // ignore all but digits and decimal points.
    for (i = 0; i < newValue.length; i++) {
        aChar = newValue.substring(i, i + 1);
        if (aChar >= "0" && aChar <= "9") {
            if (decFlag) {
                decAmount = "" + decAmount + aChar;
            }
            else {
                dolAmount = "" + dolAmount + aChar;
            }
        }
        if (aChar == ".") {
            if (decFlag) {
                dolAmount = "";
                break;
            }
            decFlag = true;
        }
    }

    // Ensure that at least a zero appears for the dollar amount.

    if (dolAmount == "") {
        dolAmount = "0";
    }
    // Strip leading zeros.
    if (dolAmount.length > 1) {
        while (dolAmount.length > 1 && dolAmount.substring(0, 1) == "0") {
            dolAmount = dolAmount.substring(1, dolAmount.length);
        }
    }

    // Round the decimal amount.
    if (decAmount.length > 2) {
        if (decAmount.substring(2, 3) > "4") {
            decAmount = parseInt(decAmount.substring(0, 2)) + 1;
            if (decAmount < 10) {
                decAmount = "0" + decAmount;
            }
            else {
                decAmount = "" + decAmount;
            }
        }
        else {
            decAmount = decAmount.substring(0, 2);
        }
        if (decAmount == 100) {
            decAmount = "00";
            dolAmount = parseInt(dolAmount) + 1;
        }
    }

    // Pad right side of decAmount
    if (decAmount.length == 1) {
        decAmount = decAmount + "0";
    }
    if (decAmount.length == 0) {
        decAmount = decAmount + "00";
    }

    // Check for negative values and reset textObj
    if (newValue.substring(0, 1) != '-' ||
        (dolAmount == "0" && decAmount == "00")) {
        textObj.value = dolAmount + "." + decAmount;

    }
    else {
        textObj.value = '-' + dolAmount + "." + decAmount;
    }
}


function checkUID(uid) {
    if (uid.length != 12) {
        return false;
    }

    var Verhoeff = {
        "d": [[0, 1, 2, 3, 4, 5, 6, 7, 8, 9],
        [1, 2, 3, 4, 0, 6, 7, 8, 9, 5],
        [2, 3, 4, 0, 1, 7, 8, 9, 5, 6],
        [3, 4, 0, 1, 2, 8, 9, 5, 6, 7],
        [4, 0, 1, 2, 3, 9, 5, 6, 7, 8],
        [5, 9, 8, 7, 6, 0, 4, 3, 2, 1],
        [6, 5, 9, 8, 7, 1, 0, 4, 3, 2],
        [7, 6, 5, 9, 8, 2, 1, 0, 4, 3],
        [8, 7, 6, 5, 9, 3, 2, 1, 0, 4],
        [9, 8, 7, 6, 5, 4, 3, 2, 1, 0]],
        "p": [[0, 1, 2, 3, 4, 5, 6, 7, 8, 9],
        [1, 5, 7, 6, 2, 8, 3, 0, 9, 4],
        [5, 8, 0, 3, 7, 9, 6, 1, 4, 2],
        [8, 9, 1, 6, 0, 4, 3, 5, 2, 7],
        [9, 4, 5, 3, 1, 2, 6, 8, 7, 0],
        [4, 2, 8, 6, 5, 7, 3, 9, 0, 1],
        [2, 7, 9, 3, 8, 0, 6, 4, 1, 5],
        [7, 0, 4, 6, 9, 1, 3, 2, 5, 8]],
        "j": [0, 4, 3, 2, 1, 5, 6, 7, 8, 9],
        "check": function (str) {
            var c = 0;
            str.replace(/\D+/g, "").split("").reverse().join("").replace(/[\d]/g, function (u, i) {
                c = Verhoeff.d[c][Verhoeff.p[i % 8][parseInt(u, 10)]];
            });
            return c;

        },
        "get": function (str) {

            var c = 0;
            str.replace(/\D+/g, "").split("").reverse().join("").replace(/[\d]/g, function (u, i) {
                c = Verhoeff.d[c][Verhoeff.p[(i + 1) % 8][parseInt(u, 10)]];
            });
            return Verhoeff.j[c];
        }
    };

    String.prototype.verhoeffCheck = (function () {
        var d = [[0, 1, 2, 3, 4, 5, 6, 7, 8, 9],
        [1, 2, 3, 4, 0, 6, 7, 8, 9, 5],
        [2, 3, 4, 0, 1, 7, 8, 9, 5, 6],
        [3, 4, 0, 1, 2, 8, 9, 5, 6, 7],
        [4, 0, 1, 2, 3, 9, 5, 6, 7, 8],
        [5, 9, 8, 7, 6, 0, 4, 3, 2, 1],
        [6, 5, 9, 8, 7, 1, 0, 4, 3, 2],
        [7, 6, 5, 9, 8, 2, 1, 0, 4, 3],
        [8, 7, 6, 5, 9, 3, 2, 1, 0, 4],
        [9, 8, 7, 6, 5, 4, 3, 2, 1, 0]];
        var p = [[0, 1, 2, 3, 4, 5, 6, 7, 8, 9],
        [1, 5, 7, 6, 2, 8, 3, 0, 9, 4],
        [5, 8, 0, 3, 7, 9, 6, 1, 4, 2],
        [8, 9, 1, 6, 0, 4, 3, 5, 2, 7],
        [9, 4, 5, 3, 1, 2, 6, 8, 7, 0],
        [4, 2, 8, 6, 5, 7, 3, 9, 0, 1],
        [2, 7, 9, 3, 8, 0, 6, 4, 1, 5],
        [7, 0, 4, 6, 9, 1, 3, 2, 5, 8]];

        return function () {
            var c = 0;
            this.replace(/\D+/g, "").split("").reverse().join("").replace(/[\d]/g, function (u, i) {
                c = d[c][p[i % 8][parseInt(u, 10)]];
            });
            return (c === 0);
        };
    })();

    if (Verhoeff['check'](uid) === 0) {
        return true;
    } else {
        return false;
    }
}