function validate(key, ControlName) {

    //getting key code of pressed key
    var keycode = (key.which) ? key.which : key.keyCode;
    var phn = document.getElementById(ControlName);

    //comparing pressed keycodes
    if (keycode == 46) {
    }

    if ((keycode < 48 || keycode > 57) && keycode != 8 && keycode != 9) {
        return false;
    }
    else {
        return true;
    }
}


function showAlert(strTextMessage) {
    alert(strTextMessage);
}

function clickButton(e, buttonid) {

    var evt = e ? e : window.event;

    var bt = document.getElementById(buttonid);

    if (bt) {

        if (evt.keyCode == 13) {

            bt.click();

            return false;

        }

    }

}

function CngliVisibility(obj) {
  
    if (typeof (obj) == 'string') {
        obj = document.getElementById(obj);
        
    }
    obj.className = "mainMenuItem";
}

function NumericKeyPressFraction(passedEvent) {
    var objEvent = (passedEvent == null) ? window.event : passedEvent; //for mozilla compatibility
    var keyCode = (navigator.appName == "Netscape") ? objEvent.which : objEvent.keyCode;
    if (keyCode < 30)
        return;
    if ((keyCode >= 48 && keyCode <= 59) || (keyCode == 46) || (keyCode == 13))
        return true;
    else
        return false;
}