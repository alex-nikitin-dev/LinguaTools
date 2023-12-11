function setDarkTheme() {
	ToggleGoogleTheme('dark');
}
function setLightTheme() {
ToggleGoogleTheme('light');
}
function ToggleGoogleTheme(desiredTheme) {
	//alert("OK");
	var toggleClassName = 'q0yked';
	var toggleElements = document.getElementsByClassName(toggleClassName);

	if (toggleElements.length > 3) {
		var darkModeToggle = toggleElements[3].children[0];
		var toggleText = darkModeToggle.innerText;

		var isDarkModeCurrentlyOn = toggleText.includes('On');
		var isLightModeCurrentlyOn = toggleText.includes('Off');

		if (desiredTheme === 'light' && !isLightModeCurrentlyOn) {
			localStorage.setItem('themeChange', 'lightPending');
			darkModeToggle.click();
		}
		else if (desiredTheme === 'dark' && !isDarkModeCurrentlyOn) {
			localStorage.setItem('themeChange', 'darkPending');
			darkModeToggle.click();
		}
	}
}
function checkThemeChange() {
	if (localStorage.getItem('themeChange') === 'lightPending') {
		// Clear the flag first to prevent an infinite loop.
		localStorage.removeItem('themeChange');

		// Then perform the second click.
		ToggleGoogleTheme('light');
	}
	else if (localStorage.getItem('themeChange') === 'darkPending') {
		// Clear the flag first to prevent an infinite loop.
		localStorage.removeItem('themeChange');

		// Then perform the second click.
		ToggleGoogleTheme('dark');
	}
}

// Call this function on page load.
document.addEventListener('DOMContentLoaded', checkThemeChange);
