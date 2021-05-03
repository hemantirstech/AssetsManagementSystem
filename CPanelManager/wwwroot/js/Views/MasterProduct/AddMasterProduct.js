


// DO NOT REMOVE : GLOBAL FUNCTIONS!

$(document).ready(function() {

	//var IsActive = "@Model.IsActive";
	//alert(IsActive);

	pageSetUp();
		var errorClass = 'invalid';
		var errorElement = 'em';

	var $checkoutForm = $('#MasterProduct-form').validate({
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
					MasterCategoryId : {
				required: true,
						min:1
					},
					MasterSubCategoryId : {
						required: true,
						min: 1
					},
					MasterBrandId: {
						required: true,
						min: 1
					},
					ProductTitle: {
						required: true
					},
					ProductSKU: {
						required: true
					},
					MasterBranchId: {
						required: true,
						min: 1
					},
					ManufacturerPartNumber: {
						required: true
					}
				},

			// Messages for form validation
			messages : {
				MasterCategoryId : {
					required: 'Please select category',
					min: 'Please select category'
				},
				MasterSubCategoryId : {
					required: 'Please select sub category',
					min: 'Please select sub category'
				},
				MasterBrandId: {
					required: 'Please select brand'
				},
				ProductTitle: {
					required: 'Please enter product title'
				},
				ProductSKU: {
					required: 'Please select sub category'
				},
				MasterBranchId: {
					required: 'Please select branch',
					min: 'Please select branch'
				},
				ManufacturerPartNumber: {
					required: 'Please enter serial no.'
				}
			},

			// Do not change code below
			errorPlacement : function(error, element) {
	error.insertAfter(element.parent());
			}
		});

	})

