

// DO NOT REMOVE : GLOBAL FUNCTIONS!

	$(document).ready(function() {

		

		pageSetUp();
			var errorClass = 'invalid';
			var errorElement = 'em';

		var $checkoutForm = $('#MasterTax-form').validate({
		errorClass		: errorClass,
				errorElement	: errorElement,
				highlight: function(element) {
		$(element).parent().removeClass('state-success').addClass("state-error");
			        $(element).removeClass('valid');
			    },
			    unhighlight: function(element) {
		$(element).parent().removeClass("state-error").addClass('state-success');
			        $(element).addClass('valid');
			    },

			// Rules for form validation
			rules: {
				TaxTitle: {
					required:	true
                },
				IsTaxPercentageAmount : {
					required :	true
				},
				TaxValue : {
					required :	true
				},
				TaxStartDate: {
					required:	true
				},
				TaxEndDate : {
					required :	true,
				},
			},

				// Messages for form validation
				messages : {
					TaxTitle : {
						required: 'Please enter Tax Title'
					},
					IsTaxPercentageAmount : {
						required: 'Please Select'
					},
					TaxValue: {
						required: 'Please enter Tax Value'
					},
					TaxStartDate: {
						required: 'Please select Tax Start Date'
					},
					TaxEndDate : {
						required : 'Please select Tax End Date',
					},
				},

				// Do not change code below
				errorPlacement : function(error, element) {
		error.insertAfter(element.parent());
				}
			});

			

		



		})

