using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Custodian.ActivityLog;
using Custodian.Models;

namespace Custodian.Platforms.Android
{ 
[BroadcastReceiver]
public class DataWedgeReceiver : BroadcastReceiver
{
    // This intent string contains the source of the data as a string
    private static string SOURCE_TAG = "com.motorolasolutions.emdk.datawedge.source";
    // This intent string contains the barcode symbology as a string
    private static string LABEL_TYPE_TAG = "com.motorolasolutions.emdk.datawedge.label_type";
    // This intent string contains the captured data as a string
    // (in the case of MSR this data string contains a concatenation of the track data)
    private static string DATA_STRING_TAG = "com.motorolasolutions.emdk.datawedge.data_string";
    // Intent Action for our operation
    public static string IntentAction = "barcodescanner.RECVR";
    public static string IntentCategory = "android.intent.category.DEFAULT";

    public event EventHandler<Barcode> scanDataReceived;

    static object Monitor = new object();

        public override void OnReceive(Context context, Intent i)
        {
            lock (Monitor)
            {
                try
                {
                    // check the intent action is for us
                    if (i.Action.Equals(IntentAction))
                    {
                        // define a string that will hold our output
                        Barcode Out = new Barcode();
                        // get the source of the data
                        Out.Source = i.GetStringExtra(SOURCE_TAG);
                        // save it to use later
                        if (Out.Source == null)
                            Out.Source = "scanner";
                        // get the data from the intent
                        Out.Data = i.GetStringExtra(DATA_STRING_TAG);
                        // let's define a variable for the data length
                        int data_len = 0;
                        // and set it to the length of the data
                        if (Out.Data != null)
                            data_len = Out.Data.Length;
                        // check if the data has come from the barcode scanner
                        if (Out.Source.Equals("scanner"))
                        {
                            // check if there is anything in the data
                            if (Out.Data != null && Out.Data.Length > 0)
                            {
                                // we have some data, so let's get it's symbology
                                Out.Type = i.GetStringExtra(LABEL_TYPE_TAG);
                                // check if the string is empty
                                if (Out.Type != null && Out.Type.Length > 0)
                                {
                                    // format of the label type string is LABEL-TYPE-SYMBOLOGY
                                    // so let's skip the LABEL-TYPE- portion to get just the symbology
                                    Out.Type = Out.Type.Substring(11);
                                }
                                else
                                {
                                    // the string was empty so let's set it to "Unknown"
                                    Out.Type = "Unknown";
                                }


                            }
                        }

                        if (scanDataReceived != null)
                        {
                            scanDataReceived(this, Out);
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.Log("1", "Exception", e.Message);
                }
            }
        }
}
}
