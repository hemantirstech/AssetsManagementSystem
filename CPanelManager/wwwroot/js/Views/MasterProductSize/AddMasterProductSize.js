

// DO NOT REMOVE : GLOBAL FUNCTIONS!

$(document).ready(function () {



	pageSetUp();
	var errorClass = 'invalid';
	var errorElement = 'em';

	var $checkoutForm = $('#MasterProductSize-form').validate({
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

			ProductSizeTitle: {
				required: true
			},
			//ProductSizeCode: {
			//	required: true
			//},
			//ProductSizeDescription: {
			//	required: true
			//},
			//MasterDepartmentId: {
			//	required: true
			//},
			
		},

		// Messages for form validation
		messages: {
			ProductSizeTitle: {
				required: 'Please enter ProductSize title'
			},
			//ProductSizeCode: {
			//	required: 'Please enter ProductSize Code'
			//},
			//ProductSizeDescription: {
			//	required: 'Please enter ProductSize Description'
			//},
			//MasterDepartmentId: {
			//	required: 'Please select Master Department Id'
			//},
		},

		// Do not change code below
		errorPlacement: function (error, element) {
			error.insertAfter(element.parent());
		}
	});







})

