

// DO NOT REMOVE : GLOBAL FUNCTIONS!

$(document).ready(function () {



	pageSetUp();
	var errorClass = 'invalid';
	var errorElement = 'em';

	var $checkoutForm = $('#MasterBankAccountType-form').validate({
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

			MasterBankAccountTypeTitle: {
				required: true
			},
			MasterBankAccountCode: {
				required: true
			},
			
		},

		// Messages for form validation
		messages: {
			MasterBankAccountTypeTitle: {
				required: 'Please enter MasterBankAccount title'
			},
			MasterBankAccountCode: {
				required: 'Please enter MasterBankAccount Code'
			},
			
		},

		// Do not change code below
		errorPlacement: function (error, element) {
			error.insertAfter(element.parent());
		}
	});







})

