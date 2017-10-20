<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class NeuralNetwork
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Result_PictureBox = New System.Windows.Forms.PictureBox()
        Me.Templates_ComboBox = New System.Windows.Forms.ComboBox()
        Me.ChangeView_Button = New System.Windows.Forms.Button()
        Me.Template_Label = New System.Windows.Forms.Label()
        Me.Epochs_TrackBar = New System.Windows.Forms.TrackBar()
        Me.Epochs_Label = New System.Windows.Forms.Label()
        Me.EpochNum_Label = New System.Windows.Forms.Label()
        Me.Train_Button = New System.Windows.Forms.Button()
        Me.Test_Button = New System.Windows.Forms.Button()
        Me.Bias_CheckBox = New System.Windows.Forms.CheckBox()
        Me.Bias_Label = New System.Windows.Forms.Label()
        Me.LearningRate_TrackBar = New System.Windows.Forms.TrackBar()
        Me.LearningRate_Label = New System.Windows.Forms.Label()
        Me.LearningRateNum_Label = New System.Windows.Forms.Label()
        Me.Momentum_Label = New System.Windows.Forms.Label()
        Me.Momentum_TrackBar = New System.Windows.Forms.TrackBar()
        Me.MomentumNum_Label = New System.Windows.Forms.Label()
        Me.TrainTest_ProgressBar = New System.Windows.Forms.ProgressBar()
        Me.ProgressBar_Label = New System.Windows.Forms.Label()
        Me.HiddenNeuronNum_Label = New System.Windows.Forms.Label()
        Me.HiddenNeurons_Label = New System.Windows.Forms.Label()
        Me.HiddenNeurons_TrackBar = New System.Windows.Forms.TrackBar()
        Me.Reset_Button = New System.Windows.Forms.Button()
        CType(Me.Result_PictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Epochs_TrackBar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LearningRate_TrackBar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Momentum_TrackBar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HiddenNeurons_TrackBar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Result_PictureBox
        '
        Me.Result_PictureBox.BackColor = System.Drawing.Color.Transparent
        Me.Result_PictureBox.Location = New System.Drawing.Point(298, 13)
        Me.Result_PictureBox.Name = "Result_PictureBox"
        Me.Result_PictureBox.Size = New System.Drawing.Size(388, 316)
        Me.Result_PictureBox.TabIndex = 0
        Me.Result_PictureBox.TabStop = False
        '
        'Templates_ComboBox
        '
        Me.Templates_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Templates_ComboBox.FormattingEnabled = True
        Me.Templates_ComboBox.Location = New System.Drawing.Point(74, 13)
        Me.Templates_ComboBox.Name = "Templates_ComboBox"
        Me.Templates_ComboBox.Size = New System.Drawing.Size(121, 21)
        Me.Templates_ComboBox.TabIndex = 1
        '
        'ChangeView_Button
        '
        Me.ChangeView_Button.Enabled = False
        Me.ChangeView_Button.Location = New System.Drawing.Point(594, 336)
        Me.ChangeView_Button.Name = "ChangeView_Button"
        Me.ChangeView_Button.Size = New System.Drawing.Size(91, 23)
        Me.ChangeView_Button.TabIndex = 2
        Me.ChangeView_Button.Text = "Change View"
        Me.ChangeView_Button.UseVisualStyleBackColor = True
        '
        'Template_Label
        '
        Me.Template_Label.AutoSize = True
        Me.Template_Label.Location = New System.Drawing.Point(14, 16)
        Me.Template_Label.Name = "Template_Label"
        Me.Template_Label.Size = New System.Drawing.Size(54, 13)
        Me.Template_Label.TabIndex = 3
        Me.Template_Label.Text = "Template:"
        '
        'Epochs_TrackBar
        '
        Me.Epochs_TrackBar.Enabled = False
        Me.Epochs_TrackBar.LargeChange = 5000
        Me.Epochs_TrackBar.Location = New System.Drawing.Point(74, 192)
        Me.Epochs_TrackBar.Maximum = 500000
        Me.Epochs_TrackBar.Minimum = 1
        Me.Epochs_TrackBar.Name = "Epochs_TrackBar"
        Me.Epochs_TrackBar.Size = New System.Drawing.Size(104, 45)
        Me.Epochs_TrackBar.SmallChange = 1000
        Me.Epochs_TrackBar.TabIndex = 4
        Me.Epochs_TrackBar.TickFrequency = 100
        Me.Epochs_TrackBar.TickStyle = System.Windows.Forms.TickStyle.None
        Me.Epochs_TrackBar.Value = 1
        '
        'Epochs_Label
        '
        Me.Epochs_Label.AutoSize = True
        Me.Epochs_Label.Location = New System.Drawing.Point(22, 196)
        Me.Epochs_Label.Name = "Epochs_Label"
        Me.Epochs_Label.Size = New System.Drawing.Size(46, 13)
        Me.Epochs_Label.TabIndex = 5
        Me.Epochs_Label.Text = "Epochs:"
        '
        'EpochNum_Label
        '
        Me.EpochNum_Label.AutoSize = True
        Me.EpochNum_Label.Location = New System.Drawing.Point(175, 196)
        Me.EpochNum_Label.Name = "EpochNum_Label"
        Me.EpochNum_Label.Size = New System.Drawing.Size(60, 13)
        Me.EpochNum_Label.TabIndex = 6
        Me.EpochNum_Label.Text = "EpochNum"
        '
        'Train_Button
        '
        Me.Train_Button.Enabled = False
        Me.Train_Button.Location = New System.Drawing.Point(25, 365)
        Me.Train_Button.Name = "Train_Button"
        Me.Train_Button.Size = New System.Drawing.Size(75, 23)
        Me.Train_Button.TabIndex = 7
        Me.Train_Button.Text = "Train"
        Me.Train_Button.UseVisualStyleBackColor = True
        '
        'Test_Button
        '
        Me.Test_Button.Enabled = False
        Me.Test_Button.Location = New System.Drawing.Point(178, 365)
        Me.Test_Button.Name = "Test_Button"
        Me.Test_Button.Size = New System.Drawing.Size(75, 23)
        Me.Test_Button.TabIndex = 8
        Me.Test_Button.Text = "Test"
        Me.Test_Button.UseVisualStyleBackColor = True
        '
        'Bias_CheckBox
        '
        Me.Bias_CheckBox.AutoSize = True
        Me.Bias_CheckBox.Enabled = False
        Me.Bias_CheckBox.Location = New System.Drawing.Point(74, 159)
        Me.Bias_CheckBox.Name = "Bias_CheckBox"
        Me.Bias_CheckBox.Size = New System.Drawing.Size(15, 14)
        Me.Bias_CheckBox.TabIndex = 9
        Me.Bias_CheckBox.UseVisualStyleBackColor = True
        '
        'Bias_Label
        '
        Me.Bias_Label.AutoSize = True
        Me.Bias_Label.Location = New System.Drawing.Point(38, 159)
        Me.Bias_Label.Name = "Bias_Label"
        Me.Bias_Label.Size = New System.Drawing.Size(30, 13)
        Me.Bias_Label.TabIndex = 10
        Me.Bias_Label.Text = "Bias:"
        '
        'LearningRate_TrackBar
        '
        Me.LearningRate_TrackBar.Enabled = False
        Me.LearningRate_TrackBar.LargeChange = 10
        Me.LearningRate_TrackBar.Location = New System.Drawing.Point(74, 57)
        Me.LearningRate_TrackBar.Maximum = 100
        Me.LearningRate_TrackBar.Name = "LearningRate_TrackBar"
        Me.LearningRate_TrackBar.Size = New System.Drawing.Size(104, 45)
        Me.LearningRate_TrackBar.SmallChange = 5
        Me.LearningRate_TrackBar.TabIndex = 11
        Me.LearningRate_TrackBar.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'LearningRate_Label
        '
        Me.LearningRate_Label.AutoSize = True
        Me.LearningRate_Label.Location = New System.Drawing.Point(17, 57)
        Me.LearningRate_Label.Name = "LearningRate_Label"
        Me.LearningRate_Label.Size = New System.Drawing.Size(51, 26)
        Me.LearningRate_Label.TabIndex = 12
        Me.LearningRate_Label.Text = "Learning" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "      Rate:"
        '
        'LearningRateNum_Label
        '
        Me.LearningRateNum_Label.AutoSize = True
        Me.LearningRateNum_Label.Location = New System.Drawing.Point(175, 60)
        Me.LearningRateNum_Label.Name = "LearningRateNum_Label"
        Me.LearningRateNum_Label.Size = New System.Drawing.Size(93, 13)
        Me.LearningRateNum_Label.TabIndex = 13
        Me.LearningRateNum_Label.Text = "LearningRateNum"
        '
        'Momentum_Label
        '
        Me.Momentum_Label.AutoSize = True
        Me.Momentum_Label.Location = New System.Drawing.Point(6, 111)
        Me.Momentum_Label.Name = "Momentum_Label"
        Me.Momentum_Label.Size = New System.Drawing.Size(62, 13)
        Me.Momentum_Label.TabIndex = 14
        Me.Momentum_Label.Text = "Momentum:"
        '
        'Momentum_TrackBar
        '
        Me.Momentum_TrackBar.Enabled = False
        Me.Momentum_TrackBar.LargeChange = 10
        Me.Momentum_TrackBar.Location = New System.Drawing.Point(74, 108)
        Me.Momentum_TrackBar.Maximum = 100
        Me.Momentum_TrackBar.Name = "Momentum_TrackBar"
        Me.Momentum_TrackBar.Size = New System.Drawing.Size(104, 45)
        Me.Momentum_TrackBar.SmallChange = 5
        Me.Momentum_TrackBar.TabIndex = 15
        Me.Momentum_TrackBar.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'MomentumNum_Label
        '
        Me.MomentumNum_Label.AutoSize = True
        Me.MomentumNum_Label.Location = New System.Drawing.Point(175, 111)
        Me.MomentumNum_Label.Name = "MomentumNum_Label"
        Me.MomentumNum_Label.Size = New System.Drawing.Size(81, 13)
        Me.MomentumNum_Label.TabIndex = 16
        Me.MomentumNum_Label.Text = "MomentumNum"
        '
        'TrainTest_ProgressBar
        '
        Me.TrainTest_ProgressBar.Location = New System.Drawing.Point(269, 365)
        Me.TrainTest_ProgressBar.Name = "TrainTest_ProgressBar"
        Me.TrainTest_ProgressBar.Size = New System.Drawing.Size(296, 23)
        Me.TrainTest_ProgressBar.TabIndex = 17
        '
        'ProgressBar_Label
        '
        Me.ProgressBar_Label.AutoSize = True
        Me.ProgressBar_Label.BackColor = System.Drawing.Color.Transparent
        Me.ProgressBar_Label.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ProgressBar_Label.Location = New System.Drawing.Point(571, 370)
        Me.ProgressBar_Label.Name = "ProgressBar_Label"
        Me.ProgressBar_Label.Size = New System.Drawing.Size(0, 13)
        Me.ProgressBar_Label.TabIndex = 18
        Me.ProgressBar_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'HiddenNeuronNum_Label
        '
        Me.HiddenNeuronNum_Label.AutoSize = True
        Me.HiddenNeuronNum_Label.Location = New System.Drawing.Point(175, 247)
        Me.HiddenNeuronNum_Label.Name = "HiddenNeuronNum_Label"
        Me.HiddenNeuronNum_Label.Size = New System.Drawing.Size(98, 13)
        Me.HiddenNeuronNum_Label.TabIndex = 21
        Me.HiddenNeuronNum_Label.Text = "HiddenNeuronNum"
        '
        'HiddenNeurons_Label
        '
        Me.HiddenNeurons_Label.AutoSize = True
        Me.HiddenNeurons_Label.Location = New System.Drawing.Point(18, 243)
        Me.HiddenNeurons_Label.Name = "HiddenNeurons_Label"
        Me.HiddenNeurons_Label.Size = New System.Drawing.Size(50, 26)
        Me.HiddenNeurons_Label.TabIndex = 20
        Me.HiddenNeurons_Label.Text = " Hidden" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Neurons:"
        '
        'HiddenNeurons_TrackBar
        '
        Me.HiddenNeurons_TrackBar.Enabled = False
        Me.HiddenNeurons_TrackBar.Location = New System.Drawing.Point(74, 243)
        Me.HiddenNeurons_TrackBar.Maximum = 100
        Me.HiddenNeurons_TrackBar.Minimum = 1
        Me.HiddenNeurons_TrackBar.Name = "HiddenNeurons_TrackBar"
        Me.HiddenNeurons_TrackBar.Size = New System.Drawing.Size(104, 45)
        Me.HiddenNeurons_TrackBar.TabIndex = 19
        Me.HiddenNeurons_TrackBar.TickFrequency = 100
        Me.HiddenNeurons_TrackBar.TickStyle = System.Windows.Forms.TickStyle.None
        Me.HiddenNeurons_TrackBar.Value = 1
        '
        'Reset_Button
        '
        Me.Reset_Button.Enabled = False
        Me.Reset_Button.Location = New System.Drawing.Point(103, 306)
        Me.Reset_Button.Name = "Reset_Button"
        Me.Reset_Button.Size = New System.Drawing.Size(75, 23)
        Me.Reset_Button.TabIndex = 22
        Me.Reset_Button.Text = "Reset"
        Me.Reset_Button.UseVisualStyleBackColor = True
        '
        'NeuralNetwork
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(698, 412)
        Me.Controls.Add(Me.Reset_Button)
        Me.Controls.Add(Me.HiddenNeuronNum_Label)
        Me.Controls.Add(Me.HiddenNeurons_Label)
        Me.Controls.Add(Me.HiddenNeurons_TrackBar)
        Me.Controls.Add(Me.ProgressBar_Label)
        Me.Controls.Add(Me.TrainTest_ProgressBar)
        Me.Controls.Add(Me.MomentumNum_Label)
        Me.Controls.Add(Me.Momentum_TrackBar)
        Me.Controls.Add(Me.Momentum_Label)
        Me.Controls.Add(Me.LearningRateNum_Label)
        Me.Controls.Add(Me.LearningRate_Label)
        Me.Controls.Add(Me.LearningRate_TrackBar)
        Me.Controls.Add(Me.Bias_Label)
        Me.Controls.Add(Me.Bias_CheckBox)
        Me.Controls.Add(Me.Test_Button)
        Me.Controls.Add(Me.Train_Button)
        Me.Controls.Add(Me.EpochNum_Label)
        Me.Controls.Add(Me.Epochs_Label)
        Me.Controls.Add(Me.Epochs_TrackBar)
        Me.Controls.Add(Me.Template_Label)
        Me.Controls.Add(Me.ChangeView_Button)
        Me.Controls.Add(Me.Templates_ComboBox)
        Me.Controls.Add(Me.Result_PictureBox)
        Me.Name = "NeuralNetwork"
        Me.Text = "Form1"
        CType(Me.Result_PictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Epochs_TrackBar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LearningRate_TrackBar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Momentum_TrackBar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HiddenNeurons_TrackBar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Result_PictureBox As PictureBox
    Friend WithEvents Templates_ComboBox As ComboBox
    Friend WithEvents ChangeView_Button As Button
    Friend WithEvents Template_Label As Label
    Friend WithEvents Epochs_TrackBar As TrackBar
    Friend WithEvents Epochs_Label As Label
    Friend WithEvents EpochNum_Label As Label
    Friend WithEvents Train_Button As Button
    Friend WithEvents Test_Button As Button
    Friend WithEvents Bias_CheckBox As CheckBox
    Friend WithEvents Bias_Label As Label
    Friend WithEvents LearningRate_TrackBar As TrackBar
    Friend WithEvents LearningRate_Label As Label
    Friend WithEvents LearningRateNum_Label As Label
    Friend WithEvents Momentum_Label As Label
    Friend WithEvents Momentum_TrackBar As TrackBar
    Friend WithEvents MomentumNum_Label As Label
    Friend WithEvents TrainTest_ProgressBar As ProgressBar
    Friend WithEvents ProgressBar_Label As Label
    Friend WithEvents HiddenNeuronNum_Label As Label
    Friend WithEvents HiddenNeurons_Label As Label
    Friend WithEvents HiddenNeurons_TrackBar As TrackBar
    Friend WithEvents Reset_Button As Button
End Class
