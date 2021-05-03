

// DO NOT REMOVE : GLOBAL FUNCTIONS!

$(document).ready(function () {



	pageSetUp();
	var errorClass = 'invalid';
	var errorElement = 'em';

	var $checkoutForm = $('#MasterIndustryType-form').validate({
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

			IndustryTypeTitle: {
				required: true
			},
			IndustryTypeCode: {
				required: true
			},
			IndustryTypeDescription: {
				required: true
			},
			
		},

		// Messages for form validation
		messages: {
			IndustryTypeTitle: {
				required: 'Please enter IndustryType Title'
			},
			IndustryTypeCode: {
				required: 'Please enter IndustryType Code'
			},
			IndustryTypeDescription: {
				required: 'Please enter IndustryType Description'
			},
			
		},

		// Do not change code below
		errorPlacement: function (error, element) {
			error.insertAfter(element.parent());
		}
	});







})

