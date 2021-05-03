

// DO NOT REMOVE : GLOBAL FUNCTIONS!

$(document).ready(function () {



	pageSetUp();
	var errorClass = 'invalid';
	var errorElement = 'em';

	var $checkoutForm = $('#MasterState-form').validate({
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

			StateTitle: {
				required: true
			},
			StateCode: {
				required: true
			},
		},

		// Messages for form validation
		messages: {
			StateTitle: {
				required: 'Please enter State title'
			},
			StateCode: {
				required: 'Please enter State Code'
			},
		},

		// Do not change code below
		errorPlacement: function (error, element) {
			error.insertAfter(element.parent());
		}
	});







})

