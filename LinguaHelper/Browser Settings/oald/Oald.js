async function bindCefSharpObjects() {
	await CefSharp.BindObjectAsync("browserItemBoundOperator");
	document.body.onmouseup = function () {
		browserItemBoundOperator.onselect(document.getSelection().toString());
	}
}
function setItem(itemId, itemValue) {
	document.getElementById(itemId).value = itemValue;
}
function browserLogin(login, pass){
	setItem('teach_email',login);
	setItem('teach_password',pass);
	document.getElementById('teach_btn-login').click();
}
function redirectAfterLogin() {
	//go to the main page
	window.location.href = "https://www.oxfordlearnersdictionaries.com/";
}
function removeElementsRecoursive(element, iterator){
	element.remove();
}

function removeAllElementsByPattern(pattern) {
	try {
		var polls = document.querySelectorAll(pattern);
		if (polls.length > 0)
			Array.prototype.forEach.call(polls, removeElementsRecoursive);
	}
	catch (e) {
		sendErrorMessage(e);
    }
}

function removeElementById(id) {
	try {
        var element = document.getElementById(id);
        if (element != null)
            element.remove();
    }
	catch (e) {
		sendErrorMessage(e);
    }
}
function clickElementById(id) {
	try {
        var element = document.getElementById(id);
        if (element != null)
            element.click();
    }
	catch (e) {
		sendErrorMessage(e);
    }
}

function sendErrorMessage(error) {
	browserItemBoundOperator.onJsError(JSON.stringify({ message: error.message, name: error.name, stack: error.stack }));
}
function deleteAd(){
	//alert("OK");
	removeAllElementsByPattern('[id ^= "ad_"]');
	removeElementById('topslot_container')
	clickElementById('onetrust-accept-btn-handler');
}
function getAllItemsToClick() {
	//alert("OK");
	return ['.sound.audio_play_button.pron-us.icon-audio'];
}

function setDarkTheme() {

}
function setLightTheme() {

}