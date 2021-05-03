

// DO NOT REMOVE : GLOBAL FUNCTIONS!

$(document).ready(function () {



	pageSetUp();
	var errorClass = 'invalid';
	var errorElement = 'em';

	var $checkoutForm = $('#MasterProductType-form').validate({
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

			ProductTypeTitle: {
				required: true
			},
			ProductTypeCode: {
				required: true
			},
			ProductTypeDescription: {
				required: true
			},
			//MasterDepartmentId: {
			//	required: true
			//},
			
		},

		// Messages for form validation
		messages: {
			ProductTypeTitle: {
				required: 'Please enter ProductType title'
			},
			ProductTypeCode: {
				required: 'Please enter ProductType Code'
			},
			ProductTypeDescription: {
				required: 'Please enter ProductType Description'
			},
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

