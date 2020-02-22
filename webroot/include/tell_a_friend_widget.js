
var Tell_A_Friend_Widget = function(config) {

    var me = this;
    var defaults = {

        renderTo: 'body',
        handler: function(e) {
            
            return false;
        }

    }
    var errorMessage = "* (required)";
    var inValidEmailMessage = "* (not valid email)";
    this.isRendered = false;

    this.opts = $.extend(defaults, config);

    var widgetContent = this.opts.widgetContent || getWidgetContent();

    var handlerFunction = this.opts.handler;

    function getWidgetContent() {

        var retStrArray = new Array();
        retStrArray.push('<div class="tellfriend-container ">');
        retStrArray.push(createEmailContainer("e-mail"));

        retStrArray.push(createReceiverEmailContainer("Receiver e-mail"));
        retStrArray.push(createMessageContainer());
        retStrArray.push(createSendButtonContainer());
        retStrArray.push("</div>");
        return $(retStrArray.join(''));

    }


    function createEmailContainer(emailStr) {

        var retStrArray = new Array();
        retStrArray.push('<div class="tellfriend-row" style="margin-top: 20px">');
        retStrArray.push('<div class="tellfriend-left-row">');
        retStrArray.push(emailStr);
        retStrArray.push('</div>');
        retStrArray.push('<div class="tellfriend-right-row">');
        retStrArray.push('<input class="tellfriend-email-address" type="text" > </input>');
        retStrArray.push('</div>');
        retStrArray.push('</div>');
        return retStrArray.join('');
    }

    function createReceiverEmailContainer(emailStr) {

        var retStrArray = new Array();
        retStrArray.push('<div class="tellfriend-row">');
        retStrArray.push('<div class="tellfriend-left-row">');
        retStrArray.push(emailStr);
        retStrArray.push('</div>');
        retStrArray.push('<div class="tellfriend-right-row">');
        retStrArray.push('<input class="tellfriend-receiver-email-address" type="text"  > </input>');
        retStrArray.push('</div>');
        retStrArray.push('</div>');
        return retStrArray.join('');
    }
    

    function createMessageContainer() {

        var retStrArray = new Array();
        retStrArray.push('<div class="tellfriend-row" style="height: auto">');
        retStrArray.push('<div class="tellfriend-left-row">');
        retStrArray.push('Message');
        retStrArray.push('</div>');
        retStrArray.push('<div class="tellfriend-right-row" style="height: 110px">');
        retStrArray.push('<textarea class="tellfriend-massage" rows="5" style="height: 100px; width: 300px"></textarea>');
        retStrArray.push('</div>');
        retStrArray.push('</div>');
        return retStrArray.join('');
    }
    
    
    function createSendButtonContainer() {

        var retStrArray = new Array();
        retStrArray.push('<div class="tellfriend-row">');
        retStrArray.push('<div class="tellfriend-left-row">');

        retStrArray.push('</div>');
        retStrArray.push('<div class="tellfriend-right-row">');
        retStrArray.push('<input  type="submit" value="Send" class="button tellfriend-send-botton"> </input>');
        retStrArray.push('</div>');
        retStrArray.push('</div>');
        return retStrArray.join('');
    }



    this.render = function() {

        $(widgetContent).appendTo(this.opts.renderTo).show();
        if (!this.isRendered) {

            this.email = $('.tellfriend-email-address', widgetContent);
            this.receiverEmail = $('.tellfriend-receiver-email-address', widgetContent);

            this.message = $('.tellfriend-massage', widgetContent);
            this.sendButton = $('.tellfriend-send-botton', widgetContent);

            this.sendButton.click(function(e) {

                widgetContent.find('span.required').remove();

                var isValid = true;
                if (me.email.val() == '') {

                    $('<span></span>')
                    .addClass('required')
                    .text(errorMessage)
                    .appendTo(me.email.parents('.tellfriend-right-row'));
                    isValid = false;

                } else if (!/.+@.+\.[a-zA-Z]{2,4}$/.test(me.email.val())) {

                    $('<span></span>')
                    .addClass('required')
                    .text(inValidEmailMessage)
                    .appendTo(me.email.parents('.tellfriend-right-row'));
                    isValid = false;

                }


                if (me.receiverEmail.val() == '') {

                    $('<span></span>')
                    .addClass('required')
                    .text(errorMessage)
                    .appendTo(me.receiverEmail.parents('.tellfriend-right-row'));
                    isValid = false;

                } else if (!/.+@.+\.[a-zA-Z]{2,4}$/.test(me.receiverEmail.val())) {

                    $('<span></span>')
                    .addClass('required')
                    .text(inValidEmailMessage)
                    .appendTo(me.receiverEmail.parents('.tellfriend-right-row'));
                    isValid = false;

                }

                if (me.message.val() == '') {

                    $('<span></span>')
                    .addClass('required')
                    .text(errorMessage)
                    .appendTo(me.message.parents('.tellfriend-right-row'));
                    isValid = false;

                }
                if (!isValid) {

                    return false;
                }

                if (!handlerFunction(e)) {

                    return false;
                }

                return false;

            });

        }


        this.isRendered = true;


    };
    this.renderTo = function(renderTo) {
        this.opts.renderTo = renderTo;
        this.render();

    };

    this.hide = function() {

        widgetContent.hide();

    };

    this.show = function() {
        if (!this.isRendered) {

            this.render();
        } else {
            widgetContent.show();
        }
    };







}

