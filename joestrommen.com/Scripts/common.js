(function() {
	function closeHeader() {

		if (!window.requestAnimationFrame) {
			DomUtil.removeClass(document.body, 'with-header');
			return;
		}

		var start = null;
		var el = document.getElementsByClassName("header")[0];
		var startHeight = el.offsetHeight;
		var duration = 150;

		var animate = function(timestamp) {
			if (!start) {
				start = timestamp;
				el.style.overflowY = 'hidden';
			}

			var progress = Math.min(duration, timestamp - start);
			var percent = progress / duration;
			var interpolated = 0.5 - Math.cos(percent * Math.PI) / 2;
			el.style.height = startHeight * (1 - interpolated) + "px";
			if (progress < duration) {
				window.requestAnimationFrame(animate);
			} else {
				DomUtil.removeClass(document.body, 'with-header');
			}
		}

		window.requestAnimationFrame(animate);
	}

	twoStage.onReady('a.email', function () {
		var email = "joe@joestrommen.com";
		for (var i = 0; i < this.length; i++) {
			var el = this[i];

			var hrefVal = 'mailto:' + email;

			var subject = el.getAttribute('data-email-subject');
			if (subject) {
				hrefVal += "?subject=" + subject;
			}

			if (!el.innerHTML) {
				el.innerHTML = email;
			}

			el.setAttribute('href', hrefVal);
		}
	});

	twoStage.onReady('a.twitter', function () {
		var handle = "strommen";
		for (var i = 0; i < this.length; i++) {
			var el = this[i];

			var hrefVal = 'https://twitter.com/' + handle;

			if (!el.innerHTML) {
				el.innerHTML = '@' + handle;
			}

			el.setAttribute('href', hrefVal);
		}
	});

	twoStage.onReady('a.submit', function () {
		for (var i = 0; i < this.length; i++) {
			var el = this[i];

			addEventListener(el, 'click', function () {
				var parentForm = DomUtil.findParent(el, function () { return this.nodeName === 'FORM'; });
				DomUtil.trigger(parentForm, 'submit');
			});
		}
	});

	twoStage.onReady("#email-signup", function () {
		var form = this[0];
		DomUtil.addEventListener(form, 'submit', function (evt) {
			evt.preventDefault();

			var email = form.querySelector("input[name=emailAddress]");
			var spamHoneypot = form.querySelector(".spam-honeypot");

			DomUtil.addClass(form, 'submitting');

			if (email.value && !spamHoneypot.value) {

				var action = form.getAttribute('action');
				var request = new XMLHttpRequest();
				request.open(form.getAttribute('method'), form.getAttribute('action'), true);
				request.onreadystatechange = function () {
					if (this.readyState === 4) {
						if (this.status >= 200 && this.status < 400) {
							form.innerHTML = '<span>' + this.responseText + '</span>';
							setTimeout(closeHeader, 5000);
						}

						DomUtil.removeClass(form, 'submitting');
					}
				};
				request.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded; charset=UTF-8');
				request.send(DomUtil.serializeInputs(form));
			}
		});
	});

	twoStage.onReady('.close-header', function () {
		for (var i = 0; i < this.length; i++) {
			var el = this[i];
			DomUtil.addEventListener(el, 'click', function () {
				//var future = new Date();
				//future.setDate(future.getDate() + 7);
				//document.cookie = "euid=null;domain=joestrommen.com;expires=" + future.toUTCString();
				closeHeader();
			});
		}
	});

	// This smooths out rendering for images with dimension attributes and width/max-width specified in CSS.
	// (without this, the height will default to 0 until the img has loaded, causing layout thrashing)
	twoStage.onReady('img[height][width]', function () {
		for (var i = 0; i < this.length; i++) {
			var el = this[i];

			var height = el.offsetWidth * el.getAttribute('height') / el.getAttribute('width');
			el.style.height = height + 'px';
		}
	});

	twoStage.onReady(function () {
		var showBodyIfFontLoaded = function (iterCount) {
			if (iterCount == 0) {
				console.log("Font still not loaded.  Oh well");
				DomUtil.removeClass(document.body, 'hidden');
				return;
			}

			var cabinWidth = document.getElementById("font-test-cabin").offsetWidth;
			var monospaceWidth = document.getElementById("font-test-monospace").offsetWidth;
			if (cabinWidth === monospaceWidth) {
				console.log("Waiting 50ms for font to load.");
				setTimeout(function () {
					showBodyIfFontLoaded(iterCount - 1);
				}, 50);
			} else {
				DomUtil.removeClass(document.body, 'hidden');
			}
		}
		showBodyIfFontLoaded(5);

		if (!DomUtil.getCookie('euid')) {
			DomUtil.addClass(document.body, 'with-header');
		}

		var bodyContents = document.getElementsByClassName("body-content");
		for (var i = 0; i < bodyContents.length; i++) {
			var el = bodyContents[i];
			DomUtil.removeClass(el, 'fade-in');
		}
	});
})();