

// DO NOT REMOVE : GLOBAL FUNCTIONS!

$(document).ready(function () {



	pageSetUp();
	var errorClass = 'invalid';
	var errorElement = 'em';

	var $checkoutForm = $('#MasterReportingHead-form').validate({
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
			ReportingHeadTitle: {
				required: true
			},
			ReportingDescription: {
				required: true
			},
		},

		// Messages for form validation
		messages: {
			ReportingHeadTitle: {
				required: 'Please enter ReportingHead Title'
			},
			ReportingDescription: {
				required: 'Please enter ReportingHead Description'
			},
		},

		// Do not change code below
		errorPlacement: function (error, element) {
			error.insertAfter(element.parent());
		}
	});







})

