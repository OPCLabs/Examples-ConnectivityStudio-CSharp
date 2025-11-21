// $Header: $ 
// Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

//
// Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
// OPC client and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
// Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
// a commercial license in order to use Online Forums, and we reply to every post.

using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using OpcLabs.EasyOpc.UA.PubSub;
using OpcLabs.EasyOpc.UA.PubSub.Extensions;
using OpcLabs.EasyOpc.UA.PubSub.Engine;
using OpcLabs.EasyOpc.UA.PubSub.OperationModel;

// ReSharper disable InconsistentNaming
// ReSharper disable StringLiteralTypo

namespace EasyOpcUAPubSubDemo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        
        private bool _subscribed;


        /// <summary>
        /// The user has pressed the "About" button. Show a message box with information about the executing assembly.
        /// </summary>
        private void aboutButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, Assembly.GetExecutingAssembly().FullName, "Assembly Name",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Event handler for the <see cref="EasyUASubscriberCore.DataSetMessage"/> event. It is invoked for every received
        /// dataset. We display the dataset data received (or the error) on the form.
        /// </summary>
        private void easyUASubscriber1_DataSetMessage(object sender, EasyUADataSetMessageEventArgs e)
        {
            Debug.Assert(!(e is null));

            errorTextBox.BackColor = e.Succeeded ? SystemColors.Control : Color.Orange;
            errorTextBox.Text = e.Succeeded ? "" : e.Exception?.GetBaseException().Message;

            uaDataSetDataControl1.Value = e.DataSetData;
        }

        /// <summary>
        /// Unsubscribe from the dataset whenever the form is closing.
        /// </summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Subscribed = false;
        }

        /// <summary>
        /// When the form loads, select the ready-made settings for UADP over MQTT.
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            readyMadeSettingsComboBox.SelectedIndex = 4;
        }

        /// <summary>
        /// Act upon a change in the "subscribed" status, i.e. perform the actual subscribe or unsubscribe. Also, enable or
        /// disable controls on the screen based on the new status.
        /// </summary>
        private void OnSubscribedChanged()
        {
            if (Subscribed)
            {
                if (!(SubscribeDataSetArguments is null))
                    easyUASubscriber1.SubscribeDataSet(SubscribeDataSetArguments);
            }
            else
                easyUASubscriber1.UnsubscribeAllDataSets();

            readyMadeSettingsComboBox.Enabled = !Subscribed;
            subscribeButton.Enabled = !Subscribed;
            unsubscribeButton.Enabled = Subscribed;
        }

        /// <summary>
        /// The user has selected a different set of read-made settings. Change the arguments for dataset subscription in
        /// accordance with the selected settings.
        /// </summary>
        private void readyMadeSettingsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UASubscribeDataSetArguments subscribeDataSetArguments = null;
            string info = null;

            switch (readyMadeSettingsComboBox.SelectedIndex)
            {
                case 0:
                    subscribeDataSetArguments = UASubscribeDataSetArguments.Default;
                    break;

                case 1:
                    subscribeDataSetArguments = new UASubscribeDataSetArguments
                    {
                        DataSetSubscriptionDescriptor =
                        {
                            CommunicationParameters =
                                {BrokerDataSetReaderTransportParameters = {QueueName = "opcuademo/json"}},
                            ConnectionDescriptor =
                            {
                                ResourceAddress = "mqtt://opcua-pubsub.demo-this.com",
                                TransportProfileUriString = UAPubSubTransportProfileUriStrings.MqttJson
                            },
                            Filter =
                            {
                                //DataSetWriterDescriptor = 4,  // not contained in the message
                                PublisherId = { ExternalValue = "32" }
                            }
                        }
                    };
                    break;

                case 2:
                    subscribeDataSetArguments = new UASubscribeDataSetArguments
                    {
                        DataSetSubscriptionDescriptor =
                        {
                            ConnectionDescriptor =
                            {
                                ResourceAddress = "opc.eth://FF-FF-FF-FF-FF-FF",
                                TransportProfileUriString = UAPubSubTransportProfileUriStrings.EthUadp
                            },
                            Filter =
                            {
                                DataSetWriterDescriptor = 4,
                                PublisherId = { ExternalValue = (Decimal)31 }
                            }
                        }
                    };
                    try
                    {
                        subscribeDataSetArguments.DataSetSubscriptionDescriptor.ConnectionDescriptor
                            .UseEthernetCaptureFile("UADemoPublisher-Ethernet.pcap");
                    }
                    catch (Exception exception)
                    {
                        errorTextBox.BackColor = Color.Orange;
                        errorTextBox.Text = exception.GetBaseException().Message;
                    }
                    break;

                case 3:
                    subscribeDataSetArguments = new UASubscribeDataSetArguments
                    {
                        DataSetSubscriptionDescriptor =
                        {
                            ConnectionDescriptor =
                            {
                                ResourceAddress = "opc.eth://FF-FF-FF-FF-FF-FF",
                                TransportProfileUriString = UAPubSubTransportProfileUriStrings.EthUadp
                            },
                            Filter =
                            {
                                DataSetWriterDescriptor = 4,
                                PublisherId = { ExternalValue = (Decimal)31 }
                            }
                        }
                    };
                    info = "In order to produce network messages for this demo, run the UADemoPublisher tool with the -eth switch. In some cases, you may have to specify the interface name to be used. Additional software may be needed.";
                    break;

                case 4:
                    subscribeDataSetArguments = new UASubscribeDataSetArguments
                    {
                        DataSetSubscriptionDescriptor =
                        {
                            CommunicationParameters =
                                {BrokerDataSetReaderTransportParameters = {QueueName = "opcuademo/uadp/none"}},
                            ConnectionDescriptor =
                            {
                                ResourceAddress = "mqtt://opcua-pubsub.demo-this.com",
                                TransportProfileUriString = UAPubSubTransportProfileUriStrings.MqttUadp
                            },
                            Filter =
                            {
                                DataSetWriterDescriptor = 4,
                                PublisherId = { ExternalValue = "32" }
                            }
                        }
                    };
                    break;

                case 5:
                    subscribeDataSetArguments = new UASubscribeDataSetArguments
                    {
                        DataSetSubscriptionDescriptor =
                        {
                            ConnectionDescriptor =
                            {
                                ResourceAddress = "opc.udp://239.0.0.1",
                                TransportProfileUriString = UAPubSubTransportProfileUriStrings.UdpUadp
                            },
                            Filter =
                            {
                                DataSetWriterDescriptor = 4,
                                PublisherId = { ExternalValue = (Decimal)31 }
                            }
                        }
                    };
                    info = "In order to produce network messages for this demo, run the UADemoPublisher tool. In some cases, you may have to specify the interface name to be used (on the publisher or subscriber side, or both).";
                    break;

                case 6:
                    subscribeDataSetArguments = new UASubscribeDataSetArguments
                    {
                        DataSetSubscriptionDescriptor =
                        {
                            ConnectionDescriptor = { Name = "FixedLayoutConnection" },
                            Filter =
                            {
                                DataSetWriterDescriptor = { Name = "SimpleWriter" },
                                WriterGroupDescriptor = { Name = "FixedLayoutGroup" }
                            },
                            ResolverDescriptor =
                            {
                                PublisherFileResourceDescriptor = "UADemoPublisher-Default.uabinary",
                                ResolverKind = UAPubSubResolverKind.PublisherFile
                            }
                        }
                    };
                    info = "In order to produce network messages for this demo, run the UADemoPublisher tool. In some cases, you may have to specify the interface name to be used (on the publisher or subscriber side, or both).";
                    break;
            }

            SubscribeDataSetArguments = subscribeDataSetArguments;
            infoLabel.Visible = !string.IsNullOrEmpty(info);
            infoLabel.Text = info;
        }

        /// <summary>
        /// The user has pressed the "Subscribe" button.
        /// </summary>
        private void subscribeButton_Click(object sender, EventArgs e)
        {
            Subscribed = true;
        }

        /// <summary>
        /// Determines whether we are currently subscribed to a dataset. Setting this property creates or removes the
        /// dataset subscription.
        /// </summary>
        private bool Subscribed
        {
            get => _subscribed;
            set
            {
                if (value == _subscribed)
                    return;
                _subscribed = value;
                OnSubscribedChanged();
            }
        }

        /// <summary>
        /// Get the arguments for dataset subscription from the control on the form, or set them to the control.
        /// </summary>
        private UASubscribeDataSetArguments SubscribeDataSetArguments
        {
            get => (UASubscribeDataSetArguments)subscribeDataSetArgumentsControl.GetControlValue();
            set => subscribeDataSetArgumentsControl.SetControlValue(value);
        }

        /// <summary>
        /// The user has pressed the "Unsubscribe" button.
        /// </summary>
        private void unsubscribeButton_Click(object sender, EventArgs e)
        {
            Subscribed = false;
        }
    }
}
