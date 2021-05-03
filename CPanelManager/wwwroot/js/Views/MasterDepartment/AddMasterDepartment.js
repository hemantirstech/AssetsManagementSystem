

// DO NOT REMOVE : GLOBAL FUNCTIONS!

$(document).ready(function () {



	pageSetUp();
	var errorClass = 'invalid';
	var errorElement = 'em';

	var $checkoutForm = $('#MasterDepartment-form').validate({
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

			DepartmentTitle: {
				required: true
			},
			//DepartmentCode: {
			//	required: true
			//},
			//DepartmentDescription: {
			//	required: true
			//},
			
		},

		// Messages for form validation
		messages: {
			DepartmentTitle: {
				required: 'Please enter Department title'
			},
			//DepartmentCode: {
			//	required: 'Please enter Department Code'
			//},
			//DepartmentDescription: {
			//	required: 'Please enter Department Description'
			//},
			
		},

		// Do not change code below
		errorPlacement: function (error, element) {
			error.insertAfter(element.parent());
		}
	});







})

