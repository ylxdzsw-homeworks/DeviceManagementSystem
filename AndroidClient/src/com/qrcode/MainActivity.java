package com.qrcode;


//import com.google.zxing.WriterException;
import com.zxing.activity.CaptureActivity;
//import com.zxing.encoding.EncodingHandler;


//import java.net.Socket;
import android.app.Activity;
import android.content.Intent;
//import android.graphics.Bitmap;
import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;
//import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;
import android.webkit.URLUtil;
import android.webkit.WebView;
import android.webkit.WebViewClient; 

public class MainActivity extends Activity {
	private TextView resultTextView;
	private EditText qrStrEditText;
	//private ImageView qrImgImageView;
	private TextView sucessTextView;
	//private int SocketNum;
	private WebView webview;
	private String url;
	//private String webadr;
	private String webchange;
	
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.main);
        
        resultTextView = (TextView) this.findViewById(R.id.tv_scan_result);
        qrStrEditText = (EditText) this.findViewById(R.id.et_qr_string);
       // qrImgImageView = (ImageView) this.findViewById(R.id.iv_qr_image);
        sucessTextView = (TextView)this.findViewById(R.id.tv_scan_issucess);
       // SocketNum = 63294;
        //webadr="192.168.1.99";
        url="http://192.168.1.99:63294/";
        //url="http://www.guokr.com";
        webchange="";
        
        
        
        Button scanBarCodeButton = (Button) this.findViewById(R.id.btn_scan_barcode);
        scanBarCodeButton.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {	
				Intent openCameraIntent = new Intent(MainActivity.this,CaptureActivity.class);
				startActivityForResult(openCameraIntent, 0);
			}
		});
        
        Button webBarButton = (Button) this.findViewById(R.id.btn_web_barcode);
        webBarButton.setOnClickListener(new OnClickListener(){
        	@Override
			public void onClick(View v) {
					
        		setContentView(R.layout.webview);  
                webview = (WebView) findViewById(R.id.webview);  
                //����WebView���ԣ��ܹ�ִ��Javascript�ű�  
                webview.getSettings().setJavaScriptEnabled(true);  
                //������Ҫ��ʾ����ҳ   
                webview.loadUrl(url);  
                //����Web��ͼ  
                webview.setWebViewClient(new HelloWebViewClient()); 
			}
        	
        });
        
        Button webqueryButton = (Button)this.findViewById(R.id.btn_query_barcode);
        webqueryButton.setOnClickListener(new OnClickListener(){
        	@Override
			public void onClick(View v) {	
        		webchange="&action=addTag&value=NeedRepair";
				Intent openCameraIntent = new Intent(MainActivity.this,CaptureActivity.class);
				startActivityForResult(openCameraIntent, 0);
			}
        });
        
        Button webrunnorButton = (Button)this.findViewById(R.id.btn_runnormal_barcode);
        webrunnorButton.setOnClickListener(new OnClickListener(){
        	@Override
			public void onClick(View v) {	
        		webchange="&action=addTag&value=Checked";
				Intent openCameraIntent = new Intent(MainActivity.this,CaptureActivity.class);
				startActivityForResult(openCameraIntent, 0);
			}
        });
        
        Button webfixnedButton = (Button)this.findViewById(R.id.btn_fixneed_barcode);
        webfixnedButton.setOnClickListener(new OnClickListener(){
        	@Override
			public void onClick(View v) {	
        		webchange="&action=addTag&value=NeedRepair";
				Intent openCameraIntent = new Intent(MainActivity.this,CaptureActivity.class);
				startActivityForResult(openCameraIntent, 0);
			}
        });
        
        
        
        Button generateQRCodeButton = (Button) this.findViewById(R.id.btn_add_qrcode);
        generateQRCodeButton.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
					String contentString = qrStrEditText.getText().toString();
					if (!contentString.equals("")) {
						webchange="&action=addTag&value="+contentString;
						Intent openCameraIntent = new Intent(MainActivity.this,CaptureActivity.class);
						startActivityForResult(openCameraIntent, 0);
			//			Bitmap qrCodeBitmap = EncodingHandler.createQRCode(contentString, 350);
			//			qrImgImageView.setImageBitmap(qrCodeBitmap);
					}else {
						Toast.makeText(MainActivity.this, "Text can not be empty", Toast.LENGTH_SHORT).show();
					}	
			}
		});
    }

    private class HelloWebViewClient extends WebViewClient {  
        @Override 
        public boolean shouldOverrideUrlLoading(WebView view, String url) {  
            view.loadUrl(url);  
            return true;  
        }  
    }  
	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
		super.onActivityResult(requestCode, resultCode, data);
		try{
	       // Socket socket = new Socket(webadr, SocketNum);  			
			
		if (resultCode == RESULT_OK) { 
			Bundle bundle = data.getExtras();
			String scanResult = bundle.getString("result");
			resultTextView.setText(scanResult);
			sucessTextView.setText("�����С���Please wait");
			//socket.getOutputStream().write(scanResult.getBytes());
			//socket.getOutputStream().flush();			
			//socket.close();
			setContentView(R.layout.webview);
            webview = (WebView) findViewById(R.id.webview);  
            //����WebView���ԣ��ܹ�ִ��Javascript�ű�  
            webview.getSettings().setJavaScriptEnabled(true);  
            //������Ҫ��ʾ����ҳ   
            if(URLUtil.isNetworkUrl(scanResult+webchange)){
            webview.loadUrl(scanResult+webchange);  
            }
            else Toast.makeText(this, "�������󡭡�Sorry", Toast.LENGTH_LONG).show();
            //����Web��ͼ  
            webview.setWebViewClient(new HelloWebViewClient());
              
			Toast.makeText(this, "�����ѷ���", Toast.LENGTH_LONG).show();
		}else{
			Toast.makeText(this, "ɨ��ʧ�ܣ�δ�õ����", Toast.LENGTH_LONG).show();
		}
		}
		catch (Exception e)
		{
			Toast.makeText(this, "����ʧ�ܣ�������", Toast.LENGTH_LONG).show();
		}
	}
}