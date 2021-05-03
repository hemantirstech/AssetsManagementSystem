


// DO NOT REMOVE : GLOBAL FUNCTIONS!

$(document).ready(function() {

	//var IsActive = "@Model.IsActive";
	//alert(IsActive);

	pageSetUp();
		var errorClass = 'invalid';
		var errorElement = 'em';

	var $checkoutForm = $('#GenCodeType-form').validate({
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

			GenCodeTypeTitle : {
							required : true
					},
			GenCodeTypePrintDesc : {
							required : true
					}
			},

			// Messages for form validation
			messages : {
				GenCodeTypeTitle : {
					required : 'Please enter gencode type title'
				},
				GenCodeTypePrintDesc : {
					required: 'Please enter gencode type code'
				}
			},

			// Do not change code below
			errorPlacement : function(error, element) {
	error.insertAfter(element.parent());
			}
		});

			

	})

