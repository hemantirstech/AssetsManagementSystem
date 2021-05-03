

// DO NOT REMOVE : GLOBAL FUNCTIONS!

$(document).ready(function () {



	pageSetUp();
	var errorClass = 'invalid';
	var errorElement = 'em';

	var $checkoutForm = $('#MasterStatus-form').validate({
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

			StatusTitle: {
				required: true
			},
			StatusCode: {
				required: true
			},
			MasterColorId: {
				required: true
			},
			
		},

		// Messages for form validation
		messages: {
			StatusTitle: {
				required: 'Please enter Status Title'
			},
			StatusCode: {
				required: 'Please enter Status Code'
			},
			MasterColorId: {
				required: 'Please enter Status ColorId'
			},
			
		},

		// Do not change code below
		errorPlacement: function (error, element) {
			error.insertAfter(element.parent());
		}
	});







})

