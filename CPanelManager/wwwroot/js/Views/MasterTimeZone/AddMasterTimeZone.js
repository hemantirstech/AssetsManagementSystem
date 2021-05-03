

// DO NOT REMOVE : GLOBAL FUNCTIONS!

$(document).ready(function () {



	pageSetUp();
	var errorClass = 'invalid';
	var errorElement = 'em';

	var $checkoutForm = $('#MasterTimeZone-form').validate({
		errorClass: errorClass,
		errorElement: errorElement,
		highlight: function (element) {
			$(element).parent().removeClass('state-success').addClass("state-error");
			$(element).removeClass('valid');
		},
		unhighlight: function (element) {
			$(element).parent().removeClass("state-error").addClass('state-success');
			$(element).addClass('valid');
		},

		// Rules for form validation
		rules: {

			TimeZoneTitle: {
				required: true
			},
			HasDst: {
				required: true
			},
			TimeZoneOffset: {
				required: true
			},
			
		},

		// Messages for form validation
		messages: {
			TimeZoneTitle: {
				required: 'Please enter Time Zone title'
			},
			HasDst: {
				required: 'Please enter '
			},
			TimeZoneOffset: {
				required: 'Please enter Time Zone Offset'
			},
			
		},

		// Do not change code below
		errorPlacement: function (error, element) {
			error.insertAfter(element.parent());
		}
	});







})

