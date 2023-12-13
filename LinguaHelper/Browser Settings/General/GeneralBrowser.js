async function bindCefSharpObjects() {
	await CefSharp.BindObjectAsync("browserItemBoundOperator");
	document.body.onmouseup = function () {
		browserItemBoundOperator.onselect(document.getSelection().toString());
	}
}
function setItem(itemId, itemValue) {
	try {
		document.getElementById(itemId).value = itemValue;
	}
	catch (e) {
		sendErrorMessage(e);
	}
}
function removeElementsRecoursive(element, iterator) {
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

function sendErrorMessage(error) {
	browserItemBoundOperator.onJsError(JSON.stringify({ message: error.message, name: error.name, stack: error.stack }));
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

function isJsInjected() {
	return true;
}
function setDefaultPageText(text) 
{
	document.getElementById("main-text").innerText = text;
}