

// DO NOT REMOVE : GLOBAL FUNCTIONS!

$(document).ready(function () {



	pageSetUp();
	var errorClass = 'invalid';
	var errorElement = 'em';

	var $checkoutForm = $('#MasterIndustryGroup-form').validate({
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

			IndustryGroupTitle: {
				required: true
			},
			IndustryGroupCode: {
				required: true
			},
			IndustryGroupDescription: {
				required: true
			},
			
		},

		// Messages for form validation
		messages: {
			IndustryGroupTitle: {
				required: 'Please enter IndustryGroup Title'
			},
			IndustryGroupCode: {
				required: 'Please enter IndustryGroup Code'
			},
			IndustryGroupDescription: {
				required: 'Please enter IndustryGroup Description'
			},
			
		},

		// Do not change code below
		errorPlacement: function (error, element) {
			error.insertAfter(element.parent());
		}
	});







})

