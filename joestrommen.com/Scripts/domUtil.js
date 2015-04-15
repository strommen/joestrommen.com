// Most code is taken from http://youmightnotneedjquery.com
var DomUtil = {
	addClass: function(el, className) {
		if (el.classList)
			el.classList.add(className);
		else
			el.className += ' ' + className;
	},

	removeClass: function (el, className) {
		if (el.classList)
			el.classList.remove(className);
		else
			el.className = el.className.replace(new RegExp('(^|\\b)' + className.split(' ').join('|') + '(\\b|$)', 'gi'), ' ');
	},

	getCookie: function (name) {
		var nameEQ = name + "=";
		var cookies = document.cookie.split(';');
		for (var i = 0; i < cookies.length; i++) {
			var cookie = cookies[i];
			var regex = new RegExp("\s*" + name + "=([^=]*)");
			var match = cookie.match(regex);
			if (match) {
				return match[1];
			}
		}
	},

	addEventListener: function (el, eventName, handler) {
		if (el.addEventListener) {
			el.addEventListener(eventName, handler);
		} else {
			el.attachEvent('on' + eventName, function () {
				handler.call(el);
			});
		}
	},

	removeEventListener: function (el, eventName, handler) {
		if (el.removeEventListener) {
			el.removeEventListener(eventName, handler);
		} else {
			el.detachEvent('on' + eventName, handler);
		}
	},

	findParent: function (el, matchFn) {
		while (true) {
			var parent = el.parentNode;
			if (!parent) {
				return;
			}
			if (matchFn.call(parent)) {
				return parent;
			}
		}
	},

	trigger: function (el, eventName) {
		if (document.createEvent) {
			var event = document.createEvent('HTMLEvents');
			event.initEvent(eventName, true, false);
			el.dispatchEvent(event);
		} else {
			el.fireEvent('on' + eventName);
		}
	},
	
	serializeInputs: function (form) {
		var paramDecls = [];
		var inputs = form.elements;
		for (var i = 0; i < inputs.length; i++) {
			var input = inputs[i];
			if (input.name) {
				paramDecls.push(input.name + '=' + input.value);
			}
		}
		return paramDecls.join("&");
	}
};