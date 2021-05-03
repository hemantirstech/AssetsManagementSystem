

// DO NOT REMOVE : GLOBAL FUNCTIONS!

$(document).ready(function () {



	pageSetUp();
	var errorClass = 'invalid';
	var errorElement = 'em';

	var $checkoutForm = $('#GenCodeMaster-form').validate({
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

			GenCodeMasterTitle: {
				required: true
			},
			GenCodeTypeId: {
				required: true
			},
			PrintDesc: {
				required: true
			},

		},

		// Messages for form validation
		messages: {
			GenCodeMasterTitle: {
				required: 'Please enter GenCode Master title'
			},
			GenCodeTypeId: {
				required: 'Please enter GenCode Master Code'
			},
			PrintDesc: {
				required: 'Please enter GenCode Master Description'
			},

		},

		// Do not change code below
		errorPlacement: function (error, element) {
			error.insertAfter(element.parent());
		}
	});







})

