

// DO NOT REMOVE : GLOBAL FUNCTIONS!

	$(document).ready(function() {

		

		pageSetUp();
			var errorClass = 'invalid';
			var errorElement = 'em';

		var $checkoutForm = $('#MasterFinancialYear-form').validate({
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
				CashBook: {
					required: true
                },
				FinancialYearDescription : {
					required : true
				},
				YearStartDate : {
					required : true
				},
				YearLocked: {
					required: true
				},
				YearEndDate: {
					required: true
				},
				CurrentYear: {
					required: true
				},
			},

				// Messages for form validation
			messages : {
				CashBook : {
					required : 'Please Enter CashBook'
				},
				FinancialYearDescription : {
					required: 'Please enter Financial Year Description'
				},
				YearStartDate: {
					required: 'Please enter Year Start Date'
				},
				YearLocked: {
					required: 'Please Enter Year Locked'
				},
				YearEndDate: {
					required: 'Please enter Year End Date'
				},
				CurrentYear: {
					required: 'Please enter Current Year'
				},
				},

				// Do not change code below
				errorPlacement : function(error, element) {
		error.insertAfter(element.parent());
				}
			});

			

		



		})

