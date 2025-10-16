using Domain.DTO;
using iText.Layout.Properties;

namespace E_LearningPlatform.Helper
{
    public static class HtmlGenerator
    {

        public static string GenerateSuccessHtml(PaymentPageInfoDto paymentPageInfoDto)
        {
            return $@"
                       <!DOCTYPE html>
            <html lang='en'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>Payment Successful</title>
                <style>
                   * {{
            margin: 0;
            padding: 0;
            box-sizing: border-box;
               }}

            body {{
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
            background:  #f0f2f5;

            padding: 20px;
        }}

        .page-selector {{
            position: fixed;
            top: 20px;
            left: 50%;
            transform: translateX(-50%);
            display: flex;
            gap: 10px;
            z-index: 1000;
        }}

        .page-btn{{
            padding: 10px 20px;
            border: none;
            background: white;
            border-radius: 25px;
            cursor: pointer;
            font-weight: 600;
            transition: all 0.3s ease;
            box-shadow: 0 4px 15px rgba(0,0,0,0.1);
        }}

        .page-btn:hover {{
            transform: translateY(-2px);
            box-shadow: 0 6px 20px rgba(0,0,0,0.15);
        }}

        .page-btn.active {{
            background: #667eea;
            color: white;
        }}

        .status-page {{
            display: none;
            width: 100%;
            max-width: 500px;
        }}

        .status-page.active {{
            display: block;
            animation: fadeIn 0.5s ease;
        }}

        @keyframes fadeIn {{
            from {{
                opacity: 0;
                transform: translateY(20px);
            }}
            to {{
                opacity: 1;
                transform: translateY(0);
            }}
        }}

        .card {{
            background: white;
            border-radius: 20px;
            padding: 50px 40px;
            text-align: center;
            box-shadow: 0 20px 60px rgba(0,0,0,0.3);
        }}
        .icon-container{{
            width: 100px;
            height: 100px;
            margin: 0 auto 35px;
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            position: relative;
            animation: bounceIn 0.7s cubic-bezier(0.68, -0.55, 0.265, 1.55);
        }}

        @keyframes bounceIn {{
            0% {{
                transform: scale(0);
            }}
            50% {{
                transform: scale(1.1);
            }}
            100% {{
                transform: scale(1);
            }}
        }}

        .success .icon-container {{
            background: linear-gradient(135deg, #00c853 0%, #00e676 100%);
            box-shadow: 0 8px 24px rgba(0, 200, 83, 0.3);
        }}

    

        .icon-container::before {{
            content: '';
            position: absolute;
            width: 120%;
            height: 120%;
            border-radius: 50%;
            animation: pulse 2s infinite;
        }}

        .success .icon-container::before {{
            background: rgba(0, 200, 83, 0.15);
        }}


      @keyframes pulse {{
            0%, 100% {{
                transform: scale(1);
                opacity: 1;
            }}
            50% {{
                transform: scale(1.15);
                opacity: 0.5;
            }}
        }}

/* .icon {{
            font-size: 50px;
            color: white;
        }} */

        .checkmark {{
            width: 50px;
            height: 50px;
            border-radius: 50%;
            display: block;
            stroke-width: 3;
            stroke: white;
            stroke-miterlimit: 10;
            margin: 0 auto;
        }}

        .checkmark-circle {{
            stroke-dasharray: 166;
            stroke-dashoffset: 166;
            stroke-width: 3;
            stroke-miterlimit: 10;
            stroke: white;
            fill: none;
            animation: stroke 0.6s cubic-bezier(0.65, 0, 0.45, 1) forwards;
        }}

        .checkmark-check {{
            transform-origin: 50% 50%;
            stroke-dasharray: 48;
            stroke-dashoffset: 48;
            animation: stroke 0.3s cubic-bezier(0.65, 0, 0.45, 1) 0.8s forwards;
        }}

        @keyframes stroke {{
            100% {{
                stroke-dashoffset: 0;
            }}
        }}

        h1 {{
            font-size: 32px;
            margin-bottom: 15px;
            color: #333;
        }}
        /* .success h1 {{
            color: #00c853;
        }} */
   .message {{
            font-size: 16px;
            color: #666;
            margin-bottom: 30px;
            line-height: 1.6;
        }}

        .details {{
            background: #f8f9fa;
            border-radius: 12px;
            padding: 20px;
            margin-bottom: 30px;
            text-align: left;
        }}

        .detail-row {{
            display: flex;
            justify-content: space-between;
            margin-bottom: 12px;
            font-size: 14px;
        }}

        .detail-row:last-child {{
            margin-bottom: 0;
        }}

        .detail-label {{
            color: #007bff;
            font-weight: 500;
        }}

        .detail-value {{
            color: #333;
            font-weight: 600;
        }}

        .btn {{
            padding: 14px 30px;
            border: none;
            border-radius: 30px;
            font-size: 16px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s ease;
            text-decoration: none;
            display: inline;
            margin: 5px;

        }}

        .btn-primary {{
            background: linear-gradient(135deg, #007bff 0%, #5db2fc 100%);
            color: white;
        }}

        .btn-primary:hover {{
            transform: translateY(-2px);
            box-shadow: 0 10px 25px #007bff6c;
        }}

        .btn-secondary {{
            background: white;
            color: #007bff;
            border: 2px solid #007bff;
        }}

        .btn-secondary:hover {{
            background: #007bff;
            color: white;
            transform: translateY(-2px);
        }}

      

        
                </style>
            </head>
            <body>
                <div class=""status-page success active"" >
        <div class=""card"">
            <div class=""icon-container"">
                <svg class=""checkmark"" xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 52 52"">
     <circle class=""checkmark-circle"" cx=""26"" cy=""26"" r=""25"" fill=""none""/>
     <path class=""checkmark-check"" fill=""none"" d=""M14.1 27.2l7.1 7.2 16.7-16.8""/>
 </svg>
            </div>
          <h1>Payment Successful!</h1>
           <p class=""message"">Your transaction has been completed successfully. A confirmation email has been sent to your registered email address.</p>
             
            <div class=""details"">
                  <div class=""detail-row"">
        <span class=""detail-label"">Transaction ID:</span>
        <span class=""detail-value"">#{paymentPageInfoDto.id}</span>
    </div>
    <div class=""detail-row"">
        <span class=""detail-label"">Amount Paid:</span>
        <span class=""detail-value"">${paymentPageInfoDto.AmountCents}</span>
    </div>
   <div class=""detail-row"">
        <span class=""detail-label"">Payment Method:</span>
        <span class=""detail-value"">${paymentPageInfoDto.paymentMethod}</span>
    </div>
   
    <div class=""detail-row"">
        <span class=""detail-label"">Date & Time:</span>
        <span class=""detail-value"">{paymentPageInfoDto.DateTime}</span>
    </div>
</div>
          

            <div class=""actions"">
                <a class=""btn btn-primary"" href='https://unluckiest-preadherently-melisa.ngrok-free.dev/api/Payments/DownloadReceipt?receiptId={paymentPageInfoDto.id}' target='_blank'>Download Receipt</a>
                <button class=""btn btn-secondary"">Back to Home</button>
            </div>
        </div>
    </div>


            </body>
            </html>
            
            ";
        }

        public static string GenerateFailedHtml(PaymentPageInfoDto paymentPageInfoDto)
        {
            return $@"
                       <!DOCTYPE html>
            <html lang='en'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>Payment Successful</title>
                <style>
                   * {{
            margin: 0;
            padding: 0;
            box-sizing: border-box;
               }}

            body {{
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
            background:  #f0f2f5;

            padding: 20px;
        }}

        .page-selector {{
            position: fixed;
            top: 20px;
            left: 50%;
            transform: translateX(-50%);
            display: flex;
            gap: 10px;
            z-index: 1000;
        }}

        .page-btn{{
            padding: 10px 20px;
            border: none;
            background: white;
            border-radius: 25px;
            cursor: pointer;
            font-weight: 600;
            transition: all 0.3s ease;
            box-shadow: 0 4px 15px rgba(0,0,0,0.1);
        }}

        .page-btn:hover {{
            transform: translateY(-2px);
            box-shadow: 0 6px 20px rgba(0,0,0,0.15);
        }}

        .page-btn.active {{
            background: #667eea;
            color: white;
        }}

        .status-page {{
            display: none;
            width: 100%;
            max-width: 500px;
        }}

        .status-page.active {{
            display: block;
            animation: fadeIn 0.5s ease;
        }}

        @keyframes fadeIn {{
            from {{
                opacity: 0;
                transform: translateY(20px);
            }}
            to {{
                opacity: 1;
                transform: translateY(0);
            }}
        }}

        .card {{
            background: white;
            border-radius: 20px;
            padding: 50px 40px;
            text-align: center;
            box-shadow: 0 20px 60px rgba(0,0,0,0.3);
        }}
        .icon-container{{
            width: 100px;
            height: 100px;
            margin: 0 auto 35px;
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            position: relative;
            animation: bounceIn 0.7s cubic-bezier(0.68, -0.55, 0.265, 1.55);
        }}

        @keyframes bounceIn {{
            0% {{
                transform: scale(0);
            }}
            50% {{
                transform: scale(1.1);
            }}
            100% {{
                transform: scale(1);
            }}
        }}

     

        .failed .icon-container {{
            background: linear-gradient(135deg, #ff3d00 0%, #ff6e40 100%);
            box-shadow: 0 8px 24px rgba(255, 61, 0, 0.3);
        }}

        

        .icon-container::before {{
            content: '';
            position: absolute;
            width: 120%;
            height: 120%;
            border-radius: 50%;
            animation: pulse 2s infinite;
        }}

        .failed .icon-container::before {{background: rgba(255, 61, 0, 0.15); }}


      @keyframes pulse {{
            0%, 100% {{
                transform: scale(1);
                opacity: 1;
            }}
            50% {{
                transform: scale(1.15);
                opacity: 0.5;
            }}
        }}

/* .icon {{
            font-size: 50px;
            color: white;
        }} */

       

        @keyframes stroke {{
            100% {{
                stroke-dashoffset: 0;
            }}
        }}

        h1 {{
            font-size: 32px;
            margin-bottom: 15px;
            color: #333;
        }}
       
   .message {{
            font-size: 16px;
            color: #666;
            margin-bottom: 30px;
            line-height: 1.6;
        }}

        .details {{
            background: #f8f9fa;
            border-radius: 12px;
            padding: 20px;
            margin-bottom: 30px;
            text-align: left;
        }}

        .detail-row {{
            display: flex;
            justify-content: space-between;
            margin-bottom: 12px;
            font-size: 14px;
        }}

        .detail-row:last-child {{
            margin-bottom: 0;
        }}

        .detail-label {{
            color: #007bff;
            font-weight: 500;
        }}

        .detail-value {{
            color: #333;
            font-weight: 600;
        }}

        .btn {{
            padding: 14px 30px;
            border: none;
            border-radius: 30px;
            font-size: 16px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s ease;
            text-decoration: none;
            display: inline;
            margin: 5px;

        }}

        .btn-primary {{
            background: linear-gradient(135deg, #007bff 0%, #5db2fc 100%);
            color: white;
        }}

        .btn-primary:hover {{
            transform: translateY(-2px);
            box-shadow: 0 10px 25px #007bff6c;
        }}

        .btn-secondary {{
            background: white;
            color: #007bff;
            border: 2px solid #007bff;
        }}

        .btn-secondary:hover {{
            background: #007bff;
            color: white;
            transform: translateY(-2px);
        }}

        .cross {{
            position: relative;
            width: 50px;
            height: 50px;
            margin: 0 auto;
        }}

        .cross-line {{
           position: absolute;
            width: 4px;
            height: 50px;
            background: white;
            left: 50%;
            top: 0;
            transform-origin: center;
            border-radius: 2px;
            filter: drop-shadow(0 2px 4px rgba(0,0,0,0.1));
        }}

        .cross-line:first-child {{
            animation: rotateLine1 0.5s ease forwards;
        }}

        .cross-line:last-child {{
            animation: rotateLine2 0.5s ease forwards;
        }}

        @keyframes rotateLine1 {{
            from {{
                transform: translateX(-50%) rotate(0deg);
            }}
            to {{
                transform: translateX(-50%) rotate(45deg);
            }}
        }}

        @keyframes rotateLine2 {{
            from {{
                transform: translateX(-50%) rotate(0deg);
            }}
            to {{
                transform: translateX(-50%) rotate(-45deg);
            }}
        }}

      

        
                </style>
            </head>
            <body>
                <div class=""status-page failed active"" id=""success-page"">
        <div class=""card"">
            <div class=""icon-container"">
                <div class=""cross"">
                    <div class=""cross-line""></div>
                    <div class=""cross-line""></div>
                </div>
            </div>
         <h1>Payment Failed</h1>
            <p class=""message"">Unfortunately, your payment could not be processed. Please check your payment details and try again.</p>
            <div class=""details"">
                <div class=""detail-row"">
      <span class=""detail-label"">Error Code:</span>
      <span class=""detail-value"">{paymentPageInfoDto.ErrorCode}</span>
  </div>
  <div class=""detail-row"">
      <span class=""detail-label"">Attempted Amount:</span>
      <span class=""detail-value"">EGP {paymentPageInfoDto.AmountCents}</span>
  </div>
  <div class=""detail-row"">
      <span class=""detail-label"">Reason:</span>
      <span class=""detail-value"">{paymentPageInfoDto.reason}</span>
  </div>
  <div class=""detail-row"">
      <span class=""detail-label"">Date & Time:</span>
      <span class=""detail-value"">{paymentPageInfoDto.DateTime}</span>
  </div>
            </div>

            <div class=""actions"">
                <button class=""btn btn-primary""><a href='' target='_blank'>Try Again</a></button>
                <button class=""btn btn-secondary"" href =''>Back to Home</button>
            </div>
        </div>
    </div>

            </body>
            </html>
            
            ";
        }

        public static string GenerateSecurityHtml(PaymentPageInfoDto paymentPageInfoDto)
        {
            return $@"
            <!DOCTYPE html>
            <html lang='en'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>Payment Successful</title>
                <style>
                   * {{
            margin: 0;
            padding: 0;
            box-sizing: border-box;
               }}

            body {{
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
            background:  #f0f2f5;

            padding: 20px;
        }}

        .page-selector {{
            position: fixed;
            top: 20px;
            left: 50%;
            transform: translateX(-50%);
            display: flex;
            gap: 10px;
            z-index: 1000;
        }}

        .page-btn{{
            padding: 10px 20px;
            border: none;
            background: white;
            border-radius: 25px;
            cursor: pointer;
            font-weight: 600;
            transition: all 0.3s ease;
            box-shadow: 0 4px 15px rgba(0,0,0,0.1);
        }}

        .page-btn:hover {{
            transform: translateY(-2px);
            box-shadow: 0 6px 20px rgba(0,0,0,0.15);
        }}

        .page-btn.active {{
            background: #667eea;
            color: white;
        }}

        .status-page {{
            display: none;
            width: 100%;
            max-width: 500px;
        }}

        .status-page.active {{
            display: block;
            animation: fadeIn 0.5s ease;
        }}

        @keyframes fadeIn {{
            from {{
                opacity: 0;
                transform: translateY(20px);
            }}
            to {{
                opacity: 1;
                transform: translateY(0);
            }}
        }}

        .card {{
            background: white;
            border-radius: 20px;
            padding: 50px 40px;
            text-align: center;
            box-shadow: 0 20px 60px rgba(0,0,0,0.3);
        }}
        .icon-container{{
            width: 100px;
            height: 100px;
            margin: 0 auto 35px;
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            position: relative;
            animation: bounceIn 0.7s cubic-bezier(0.68, -0.55, 0.265, 1.55);
        }}

        @keyframes bounceIn {{
            0% {{
                transform: scale(0);
            }}
            50% {{
                transform: scale(1.1);
            }}
            100% {{
                transform: scale(1);
            }}
        }}

     

        .security .icon-container {{
                background: linear-gradient(135deg, #ffa726 0%, #ffb74d 100%);
            box-shadow: 0 8px 24px rgba(255, 167, 38, 0.3);
        }}

        

        .icon-container::before {{
            content: '';
            position: absolute;
            width: 120%;
            height: 120%;
            border-radius: 50%;
            animation: pulse 2s infinite;
        }}

.security .icon-container::before 
{{background: rgba(255, 167, 38, 0.15);
        }}

      @keyframes pulse {{
            0%, 100% {{
                transform: scale(1);
                opacity: 1;
            }}
            50% {{
                transform: scale(1.15);
                opacity: 0.5;
            }}
        }}



       

        @keyframes stroke {{
            100% {{
                stroke-dashoffset: 0;
            }}
        }}

        h1 {{
            font-size: 32px;
            margin-bottom: 15px;
            color: #333;
        }}
       
   .message {{
            font-size: 16px;
            color: #666;
            margin-bottom: 30px;
            line-height: 1.6;
        }}

        .details {{
            background: #f8f9fa;
            border-radius: 12px;
            padding: 20px;
            margin-bottom: 30px;
            text-align: left;
        }}

        .detail-row {{
            display: flex;
            justify-content: space-between;
            margin-bottom: 12px;
            font-size: 14px;
        }}

        .detail-row:last-child {{
            margin-bottom: 0;
        }}

        .detail-label {{
            color: #007bff;
            font-weight: 500;
        }}

        .detail-value {{
            color: #333;
            font-weight: 600;
        }}

        .btn {{
            padding: 14px 30px;
            border: none;
            border-radius: 30px;
            font-size: 16px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s ease;
            text-decoration: none;
            display: inline;
            margin: 5px;

        }}

        .btn-primary {{
            background: linear-gradient(135deg, #007bff 0%, #5db2fc 100%);
            color: white;
        }}

        .btn-primary:hover {{
            transform: translateY(-2px);
            box-shadow: 0 10px 25px #007bff6c;
        }}

        .btn-secondary {{
            background: white;
            color: #007bff;
            border: 2px solid #007bff;
        }}

        .btn-secondary:hover {{
            background: #007bff;
            color: white;
            transform: translateY(-2px);
        }}

         .shield
            {{
            position: relative;
            width: 50px;
            height: 58px;
            margin: 0 auto;
             }}

        .shield-bg 
            {{
            width: 50px;
            height: 58px;
            background: white;
            clip-path: polygon(50% 0%, 100% 20%, 100% 70%, 50% 100%, 0% 70%, 0% 20%);
            filter: drop-shadow(0 2px 4px rgba(0,0,0,0.1));
            }}

        .shield-exclamation
                 {{
           position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            color: #ffa726;
            font-weight: 900;
            font-size: 36px;
            line-height: 1;
                }}        
                </style>
            </head>
            <body>
                <div class=""status-page success active"" id=""success-page"">
        <div class=""card"">
          <div class=""icon-container"">
                  <div class=""icon-container"">
                <div class=""shield"">
                    <div class=""shield-bg""></div>
                    <div class=""shield-exclamation"">!</div>
                </div>
            </div>
            <h1>Security Validation Failed</h1>
            <p class=""message"">Your transaction was blocked due to security reasons. This could be due to unusual activity or failed verification.</p>
            
            <div class=""details"">
                 <div class=""detail-row"">
      <span class=""detail-label"">Security Code:</span>
      <span class=""detail-value"">{paymentPageInfoDto.ErrorCode}</span>
  </div>
  <div class=""detail-row"">
      <span class=""detail-label"">Attempted Amount:</span>
      <span class=""detail-value"">EGP {paymentPageInfoDto.AmountCents}</span>
  </div>
  <div class=""detail-row"">
      <span class=""detail-label"">Reason:</span>
      <span class=""detail-value"">{paymentPageInfoDto.reason}</span>
  </div>
  <div class=""detail-row"">
      <span class=""detail-label"">Date & Time:</span>
      <span class=""detail-value"">{paymentPageInfoDto.DateTime}</span>
  </div>
            </div>

            <div class=""actions"">
                <button class=""btn btn-primary""><a href='' target='_blank'>Try Again</a></button>
                <button class=""btn btn-secondary"" href =''>Back to Home</button>
            </div>
        </div>
    </div>

            </body>
            </html>
            
            ";
        }
    }
}
