

// DO NOT REMOVE : GLOBAL FUNCTIONS!

$(document).ready(function () {



	pageSetUp();
	var errorClass = 'invalid';
	var errorElement = 'em';

	var $checkoutForm = $('#MasterDesignation-form').validate({
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

			DesignationTitle: {
				required: true
			},
			//DesignationCode: {
			//	required: true
			//},
			//DesignationDescription: {
			//	required: true
			//},
			MasterDepartmentId: {
				required: true
			},
			
		},

		// Messages for form validation
		messages: {
			DesignationTitle: {
				required: 'Please enter Designation title'
			},
			//DesignationCode: {
			//	required: 'Please enter Designation Code'
			//},
			//DesignationDescription: {
			//	required: 'Please enter Designation Description'
			//},
			MasterDepartmentId: {
				required: 'Please select Master Department Id'
			},
		},

		// Do not change code below
		errorPlacement: function (error, element) {
			error.insertAfter(element.parent());
		}
	});







})

