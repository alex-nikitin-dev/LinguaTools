
function browserLogin(login, pass){
	setItem('teach_email',login);
	setItem('teach_password',pass);
	document.getElementById('teach_btn-login').click();
}
function redirectAfterLogin() {
	//go to the main page
	window.location.href = "https://www.oxfordlearnersdictionaries.com/";
}

function deleteAd(){
	//alert("OK");
	removeAllElementsByPattern('[id ^= "ad_"]');
	removeElementById('topslot_container')
}

function acceptAllCookies() {
	//alert("acceptAllCookies");
clickElementById('onetrust-accept-btn-handler');
}

function setDarkTheme() {

}
function setLightTheme() {

}

