﻿

// DO NOT REMOVE : GLOBAL FUNCTIONS!

$(document).ready(function () {



	pageSetUp();
	var errorClass = 'invalid';
	var errorElement = 'em';

	var $checkoutForm = $('#MasterRegion-form').validate({
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
			MasterRegionTitle: {
				required: true
			},
		},

		// Messages for form validation
		messages: {
			MasterRegionTitle: {
				required: 'Please enter Region Title'
			},
		},

		// Do not change code below
		errorPlacement: function (error, element) {
			error.insertAfter(element.parent());
		}
	});







})

