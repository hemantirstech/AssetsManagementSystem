

// DO NOT REMOVE : GLOBAL FUNCTIONS!

$(document).ready(function () {



	pageSetUp();
	var errorClass = 'invalid';
	var errorElement = 'em';

	var $checkoutForm = $('#MasterCountry-form').validate({
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

			CountryTitle: {
				required: true
			},
			CountryDialCode: {
				required: true
			},
			CountryFlag: {
				required: true
			},
			
		},

		// Messages for form validation
		messages: {
			CountryTitle: {
				required: 'Please enter Country title'
			},
			CountryDialCode: {
				required: 'Please enter Country Code'
			},
			CountryFlag: {
				required: 'Please enter Country Description'
			},
			
		},

		// Do not change code below
		errorPlacement: function (error, element) {
			error.insertAfter(element.parent());
		}
	});







})

