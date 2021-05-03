

// DO NOT REMOVE : GLOBAL FUNCTIONS!

$(document).ready(function () {



	pageSetUp();
	var errorClass = 'invalid';
	var errorElement = 'em';

	var $checkoutForm = $('#MasterCity-form').validate({
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

			CityTitle: {
				required: true
			},
			//BrandCode: {
			//	required: true
			//},
			//BrandDescription: {
			//	required: true
			//},
			//MasterDepartmentId: {
			//	required: true
			//},
			
		},

		// Messages for form validation
		messages: {
			CityTitle: {
				required: 'Please enter City title'
			},
			//BrandCode: {
			//	required: 'Please enter Brand Code'
			//},
			//BrandDescription: {
			//	required: 'Please enter Brand Description'
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

