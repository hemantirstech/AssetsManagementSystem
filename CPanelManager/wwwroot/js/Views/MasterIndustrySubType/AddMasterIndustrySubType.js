

// DO NOT REMOVE : GLOBAL FUNCTIONS!

$(document).ready(function () {



	pageSetUp();
	var errorClass = 'invalid';
	var errorElement = 'em';

	var $checkoutForm = $('#MasterIndustrySubType-form').validate({
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

			IndustrySubTypeTitle: {
				required: true
			},
			IndustrySubTypeCode: {
				required: true
			},
			IndustrySubTypeDescription: {
				required: true
			},
			
		},

		// Messages for form validation
		messages: {
			IndustryGroupTitle: {
				required: 'Please enter IndustrySubType Title'
			},
			IndustrySubTypeCode: {
				required: 'Please enter IndustrySubType Code'
			},
			IndustrySubTypeDescription: {
				required: 'Please enter IndustrySubType Description'
			},
			
		},

		// Do not change code below
		errorPlacement: function (error, element) {
			error.insertAfter(element.parent());
		}
	});







})

