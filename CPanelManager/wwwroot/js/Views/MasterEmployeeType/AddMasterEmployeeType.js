

// DO NOT REMOVE : GLOBAL FUNCTIONS!

	$(document).ready(function() {

		

		pageSetUp();
			var errorClass = 'invalid';
			var errorElement = 'em';

		var $checkoutForm = $('#MasterEmployeeType-form').validate({
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
				EmployeeTypeTitle: {
					required: true
                },
				EmpTypCode : {
								required : true
				},
				Remark : {
								required : true
				},
			},

				// Messages for form validation
				messages : {
					EmployeeTypeTitle : {
						required : 'Please Enter Employee Type Title'
					},
					EmpTypCode : {
						required: 'Please enter EmpType Code'
					},
					Remark: {
						required: 'Please enter Remark'
					},
				},

				// Do not change code below
				errorPlacement : function(error, element) {
		error.insertAfter(element.parent());
				}
			});

			

		



		})

