

// DO NOT REMOVE : GLOBAL FUNCTIONS!

$(document).ready(function () {



	pageSetUp();
	var errorClass = 'invalid';
	var errorElement = 'em';

	var $checkoutForm = $('#MasterSubCategory-form').validate({
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

			SubCategoryTitle: {
				required: true
			},
			SubCategoryCode: {
				required: true
			},
			MasterCategoryId: {
				required: true
			}			
		},

		// Messages for form validation
		messages: {
			SubCategoryTitle: {
				required: 'Please enter sub-category title'
			},
			SubCategoryCode: {
				required: 'Please enter sub-categor code'
			},
			MasterCategoryId: {
				required: 'Please select master category id'
			}
		},

		// Do not change code below
		errorPlacement: function (error, element) {
			error.insertAfter(element.parent());
		}
	});







})

