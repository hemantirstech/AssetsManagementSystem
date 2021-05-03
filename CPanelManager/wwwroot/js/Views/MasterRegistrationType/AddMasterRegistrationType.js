

// DO NOT REMOVE : GLOBAL FUNCTIONS!

$(document).ready(function () {



	pageSetUp();
	var errorClass = 'invalid';
	var errorElement = 'em';

	var $checkoutForm = $('#MasterRegistrationType-form').validate({
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

			MasterRegistrationTypeTitle: {
				required: true
			},
			MasterRegistrationExpertType: {
				required: true
			},
			MasterRegistrationCode: {
				required: true
			},
			
		},

		// Messages for form validation
		messages: {
			MasterRegistrationTypeTitle: {
				required: 'Please enter RegistrationType title'
			},
			MasterRegistrationExpertType: {
				required: 'Please enter RegistrationType ExpertType'
			},
			MasterRegistrationCode: {
				required: 'Please enter RegistrationType Code'
			},
			
		},

		// Do not change code below
		errorPlacement: function (error, element) {
			error.insertAfter(element.parent());
		}
	});







})

