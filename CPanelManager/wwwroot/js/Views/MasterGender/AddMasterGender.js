

// DO NOT REMOVE : GLOBAL FUNCTIONS!

$(document).ready(function () {



	pageSetUp();
	var errorClass = 'invalid';
	var errorElement = 'em';

	var $checkoutForm = $('#MasterGender-form').validate({
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

			GenderTitle: {
				required: true
			},
			Gendercode: {
				required: true
			},
			GenderIcon: {
				required: true
			},
			
		},

		// Messages for form validation
		messages: {
			GenderTitle: {
				required: 'Please enter Gender title'
			},
			Gendercode: {
				required: 'Please enter Gender Code'
			},
			GenderIcon: {
				required: 'Please enter Gender Icon'
			},
			
		},

		// Do not change code below
		errorPlacement: function (error, element) {
			error.insertAfter(element.parent());
		}
	});







})

