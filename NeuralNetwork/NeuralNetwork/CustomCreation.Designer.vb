<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CustomCreation
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.InputNeurons_Label = New System.Windows.Forms.Label()
        Me.HiddenNeurons_Label = New System.Windows.Forms.Label()
        Me.OutputNeurons_Label = New System.Windows.Forms.Label()
        Me.InputNeurons_Trackbar = New System.Windows.Forms.TrackBar()
        Me.HiddenNeurons_Trackbar = New System.Windows.Forms.TrackBar()
        Me.OutputNeurons_Trackbar = New System.Windows.Forms.TrackBar()
        Me.INSize_Label = New System.Windows.Forms.Label()
        Me.HNSize_Label = New System.Windows.Forms.Label()
        Me.ONSize_Label = New System.Windows.Forms.Label()
        Me.LearningRate_Label = New System.Windows.Forms.Label()
        Me.LearningRate_Trackbar = New System.Windows.Forms.TrackBar()
        Me.LRValue_Label = New System.Windows.Forms.Label()
        Me.Momentum_Label = New System.Windows.Forms.Label()
        Me.Momentum_Trackbar = New System.Windows.Forms.TrackBar()
        Me.MomentumValue_Label = New System.Windows.Forms.Label()
        Me.Bias_Label = New System.Windows.Forms.Label()
        Me.Bias_CheckBox = New System.Windows.Forms.CheckBox()
        Me.TrainData_Label = New System.Windows.Forms.Label()
        Me.TrainData_TextBox = New System.Windows.Forms.TextBox()
        Me.TrainBrowse_Button = New System.Windows.Forms.Button()
        Me.TestBrowse_Button = New System.Windows.Forms.Button()
        Me.TestData_TextBox = New System.Windows.Forms.TextBox()
        Me.TestData_Label = New System.Windows.Forms.Label()
        Me.Finish_Button = New System.Windows.Forms.Button()
        CType(Me.InputNeurons_Trackbar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HiddenNeurons_Trackbar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OutputNeurons_Trackbar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LearningRate_Trackbar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Momentum_Trackbar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'InputNeurons_Label
        '
        Me.InputNeurons_Label.AutoSize = True
        Me.InputNeurons_Label.Location = New System.Drawing.Point(19, 9)
        Me.InputNeurons_Label.Name = "InputNeurons_Label"
        Me.InputNeurons_Label.Size = New System.Drawing.Size(77, 13)
        Me.InputNeurons_Label.TabIndex = 0
        Me.InputNeurons_Label.Text = "Input Neurons:"
        '
        'HiddenNeurons_Label
        '
        Me.HiddenNeurons_Label.AutoSize = True
        Me.HiddenNeurons_Label.Location = New System.Drawing.Point(9, 45)
        Me.HiddenNeurons_Label.Name = "HiddenNeurons_Label"
        Me.HiddenNeurons_Label.Size = New System.Drawing.Size(87, 13)
        Me.HiddenNeurons_Label.TabIndex = 1
        Me.HiddenNeurons_Label.Text = "Hidden Neurons:"
        '
        'OutputNeurons_Label
        '
        Me.OutputNeurons_Label.AutoSize = True
        Me.OutputNeurons_Label.Location = New System.Drawing.Point(11, 83)
        Me.OutputNeurons_Label.Name = "OutputNeurons_Label"
        Me.OutputNeurons_Label.Size = New System.Drawing.Size(85, 13)
        Me.OutputNeurons_Label.TabIndex = 2
        Me.OutputNeurons_Label.Text = "Output Neurons:"
        '
        'InputNeurons_Trackbar
        '
        Me.InputNeurons_Trackbar.Location = New System.Drawing.Point(102, 6)
        Me.InputNeurons_Trackbar.Maximum = 100
        Me.InputNeurons_Trackbar.Minimum = 1
        Me.InputNeurons_Trackbar.Name = "InputNeurons_Trackbar"
        Me.InputNeurons_Trackbar.Size = New System.Drawing.Size(104, 45)
        Me.InputNeurons_Trackbar.TabIndex = 3
        Me.InputNeurons_Trackbar.TickStyle = System.Windows.Forms.TickStyle.None
        Me.InputNeurons_Trackbar.Value = 1
        '
        'HiddenNeurons_Trackbar
        '
        Me.HiddenNeurons_Trackbar.Location = New System.Drawing.Point(102, 43)
        Me.HiddenNeurons_Trackbar.Maximum = 100
        Me.HiddenNeurons_Trackbar.Minimum = 1
        Me.HiddenNeurons_Trackbar.Name = "HiddenNeurons_Trackbar"
        Me.HiddenNeurons_Trackbar.Size = New System.Drawing.Size(104, 45)
        Me.HiddenNeurons_Trackbar.TabIndex = 4
        Me.HiddenNeurons_Trackbar.TickStyle = System.Windows.Forms.TickStyle.None
        Me.HiddenNeurons_Trackbar.Value = 1
        '
        'OutputNeurons_Trackbar
        '
        Me.OutputNeurons_Trackbar.Location = New System.Drawing.Point(102, 83)
        Me.OutputNeurons_Trackbar.Maximum = 1
        Me.OutputNeurons_Trackbar.Minimum = 1
        Me.OutputNeurons_Trackbar.Name = "OutputNeurons_Trackbar"
        Me.OutputNeurons_Trackbar.Size = New System.Drawing.Size(104, 45)
        Me.OutputNeurons_Trackbar.TabIndex = 5
        Me.OutputNeurons_Trackbar.TickStyle = System.Windows.Forms.TickStyle.None
        Me.OutputNeurons_Trackbar.Value = 1
        '
        'INSize_Label
        '
        Me.INSize_Label.AutoSize = True
        Me.INSize_Label.Location = New System.Drawing.Point(213, 9)
        Me.INSize_Label.Name = "INSize_Label"
        Me.INSize_Label.Size = New System.Drawing.Size(51, 13)
        Me.INSize_Label.TabIndex = 6
        Me.INSize_Label.Text = "InputSize"
        '
        'HNSize_Label
        '
        Me.HNSize_Label.AutoSize = True
        Me.HNSize_Label.Location = New System.Drawing.Point(213, 45)
        Me.HNSize_Label.Name = "HNSize_Label"
        Me.HNSize_Label.Size = New System.Drawing.Size(61, 13)
        Me.HNSize_Label.TabIndex = 7
        Me.HNSize_Label.Text = "HiddenSize"
        '
        'ONSize_Label
        '
        Me.ONSize_Label.AutoSize = True
        Me.ONSize_Label.Location = New System.Drawing.Point(213, 85)
        Me.ONSize_Label.Name = "ONSize_Label"
        Me.ONSize_Label.Size = New System.Drawing.Size(59, 13)
        Me.ONSize_Label.TabIndex = 8
        Me.ONSize_Label.Text = "OutputSize"
        '
        'LearningRate_Label
        '
        Me.LearningRate_Label.AutoSize = True
        Me.LearningRate_Label.Location = New System.Drawing.Point(19, 125)
        Me.LearningRate_Label.Name = "LearningRate_Label"
        Me.LearningRate_Label.Size = New System.Drawing.Size(77, 13)
        Me.LearningRate_Label.TabIndex = 9
        Me.LearningRate_Label.Text = "Learning Rate:"
        '
        'LearningRate_Trackbar
        '
        Me.LearningRate_Trackbar.LargeChange = 10
        Me.LearningRate_Trackbar.Location = New System.Drawing.Point(102, 124)
        Me.LearningRate_Trackbar.Maximum = 100
        Me.LearningRate_Trackbar.Name = "LearningRate_Trackbar"
        Me.LearningRate_Trackbar.Size = New System.Drawing.Size(104, 45)
        Me.LearningRate_Trackbar.SmallChange = 5
        Me.LearningRate_Trackbar.TabIndex = 10
        Me.LearningRate_Trackbar.TickStyle = System.Windows.Forms.TickStyle.None
        Me.LearningRate_Trackbar.Value = 1
        '
        'LRValue_Label
        '
        Me.LRValue_Label.AutoSize = True
        Me.LRValue_Label.Location = New System.Drawing.Point(213, 125)
        Me.LRValue_Label.Name = "LRValue_Label"
        Me.LRValue_Label.Size = New System.Drawing.Size(48, 13)
        Me.LRValue_Label.TabIndex = 11
        Me.LRValue_Label.Text = "LRValue"
        '
        'Momentum_Label
        '
        Me.Momentum_Label.AutoSize = True
        Me.Momentum_Label.Location = New System.Drawing.Point(34, 166)
        Me.Momentum_Label.Name = "Momentum_Label"
        Me.Momentum_Label.Size = New System.Drawing.Size(62, 13)
        Me.Momentum_Label.TabIndex = 12
        Me.Momentum_Label.Text = "Momentum:"
        '
        'Momentum_Trackbar
        '
        Me.Momentum_Trackbar.LargeChange = 10
        Me.Momentum_Trackbar.Location = New System.Drawing.Point(102, 164)
        Me.Momentum_Trackbar.Maximum = 100
        Me.Momentum_Trackbar.Name = "Momentum_Trackbar"
        Me.Momentum_Trackbar.Size = New System.Drawing.Size(104, 45)
        Me.Momentum_Trackbar.SmallChange = 5
        Me.Momentum_Trackbar.TabIndex = 13
        Me.Momentum_Trackbar.TickStyle = System.Windows.Forms.TickStyle.None
        Me.Momentum_Trackbar.Value = 1
        '
        'MomentumValue_Label
        '
        Me.MomentumValue_Label.AutoSize = True
        Me.MomentumValue_Label.Location = New System.Drawing.Point(213, 166)
        Me.MomentumValue_Label.Name = "MomentumValue_Label"
        Me.MomentumValue_Label.Size = New System.Drawing.Size(57, 13)
        Me.MomentumValue_Label.TabIndex = 14
        Me.MomentumValue_Label.Text = "MomValue"
        '
        'Bias_Label
        '
        Me.Bias_Label.AutoSize = True
        Me.Bias_Label.Location = New System.Drawing.Point(66, 204)
        Me.Bias_Label.Name = "Bias_Label"
        Me.Bias_Label.Size = New System.Drawing.Size(30, 13)
        Me.Bias_Label.TabIndex = 15
        Me.Bias_Label.Text = "Bias:"
        '
        'Bias_CheckBox
        '
        Me.Bias_CheckBox.AutoSize = True
        Me.Bias_CheckBox.Location = New System.Drawing.Point(111, 203)
        Me.Bias_CheckBox.Name = "Bias_CheckBox"
        Me.Bias_CheckBox.Size = New System.Drawing.Size(15, 14)
        Me.Bias_CheckBox.TabIndex = 16
        Me.Bias_CheckBox.UseVisualStyleBackColor = True
        '
        'TrainData_Label
        '
        Me.TrainData_Label.AutoSize = True
        Me.TrainData_Label.Location = New System.Drawing.Point(22, 241)
        Me.TrainData_Label.Name = "TrainData_Label"
        Me.TrainData_Label.Size = New System.Drawing.Size(74, 13)
        Me.TrainData_Label.TabIndex = 17
        Me.TrainData_Label.Text = "Training Data:"
        '
        'TrainData_TextBox
        '
        Me.TrainData_TextBox.Location = New System.Drawing.Point(102, 238)
        Me.TrainData_TextBox.Name = "TrainData_TextBox"
        Me.TrainData_TextBox.Size = New System.Drawing.Size(133, 20)
        Me.TrainData_TextBox.TabIndex = 18
        '
        'TrainBrowse_Button
        '
        Me.TrainBrowse_Button.Location = New System.Drawing.Point(241, 238)
        Me.TrainBrowse_Button.Name = "TrainBrowse_Button"
        Me.TrainBrowse_Button.Size = New System.Drawing.Size(60, 20)
        Me.TrainBrowse_Button.TabIndex = 19
        Me.TrainBrowse_Button.Text = "Browse"
        Me.TrainBrowse_Button.UseVisualStyleBackColor = True
        '
        'TestBrowse_Button
        '
        Me.TestBrowse_Button.Location = New System.Drawing.Point(241, 264)
        Me.TestBrowse_Button.Name = "TestBrowse_Button"
        Me.TestBrowse_Button.Size = New System.Drawing.Size(60, 20)
        Me.TestBrowse_Button.TabIndex = 22
        Me.TestBrowse_Button.Text = "Browse"
        Me.TestBrowse_Button.UseVisualStyleBackColor = True
        '
        'TestData_TextBox
        '
        Me.TestData_TextBox.Location = New System.Drawing.Point(102, 264)
        Me.TestData_TextBox.Name = "TestData_TextBox"
        Me.TestData_TextBox.Size = New System.Drawing.Size(133, 20)
        Me.TestData_TextBox.TabIndex = 21
        '
        'TestData_Label
        '
        Me.TestData_Label.AutoSize = True
        Me.TestData_Label.Location = New System.Drawing.Point(25, 267)
        Me.TestData_Label.Name = "TestData_Label"
        Me.TestData_Label.Size = New System.Drawing.Size(71, 13)
        Me.TestData_Label.TabIndex = 20
        Me.TestData_Label.Text = "Testing Data:"
        '
        'Finish_Button
        '
        Me.Finish_Button.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Finish_Button.Location = New System.Drawing.Point(226, 299)
        Me.Finish_Button.Name = "Finish_Button"
        Me.Finish_Button.Size = New System.Drawing.Size(75, 23)
        Me.Finish_Button.TabIndex = 23
        Me.Finish_Button.Text = "Finish"
        Me.Finish_Button.UseVisualStyleBackColor = True
        '
        'CustomCreation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(311, 334)
        Me.Controls.Add(Me.Finish_Button)
        Me.Controls.Add(Me.TestBrowse_Button)
        Me.Controls.Add(Me.TestData_TextBox)
        Me.Controls.Add(Me.TestData_Label)
        Me.Controls.Add(Me.TrainBrowse_Button)
        Me.Controls.Add(Me.TrainData_TextBox)
        Me.Controls.Add(Me.TrainData_Label)
        Me.Controls.Add(Me.Bias_CheckBox)
        Me.Controls.Add(Me.Bias_Label)
        Me.Controls.Add(Me.MomentumValue_Label)
        Me.Controls.Add(Me.Momentum_Trackbar)
        Me.Controls.Add(Me.Momentum_Label)
        Me.Controls.Add(Me.LRValue_Label)
        Me.Controls.Add(Me.LearningRate_Trackbar)
        Me.Controls.Add(Me.LearningRate_Label)
        Me.Controls.Add(Me.ONSize_Label)
        Me.Controls.Add(Me.HNSize_Label)
        Me.Controls.Add(Me.INSize_Label)
        Me.Controls.Add(Me.OutputNeurons_Trackbar)
        Me.Controls.Add(Me.HiddenNeurons_Trackbar)
        Me.Controls.Add(Me.InputNeurons_Trackbar)
        Me.Controls.Add(Me.OutputNeurons_Label)
        Me.Controls.Add(Me.HiddenNeurons_Label)
        Me.Controls.Add(Me.InputNeurons_Label)
        Me.Name = "CustomCreation"
        Me.Text = "CustomCreation"
        CType(Me.InputNeurons_Trackbar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HiddenNeurons_Trackbar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OutputNeurons_Trackbar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LearningRate_Trackbar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Momentum_Trackbar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents InputNeurons_Label As Label
    Friend WithEvents HiddenNeurons_Label As Label
    Friend WithEvents OutputNeurons_Label As Label
    Friend WithEvents InputNeurons_Trackbar As TrackBar
    Friend WithEvents HiddenNeurons_Trackbar As TrackBar
    Friend WithEvents OutputNeurons_Trackbar As TrackBar
    Friend WithEvents INSize_Label As Label
    Friend WithEvents HNSize_Label As Label
    Friend WithEvents ONSize_Label As Label
    Friend WithEvents LearningRate_Label As Label
    Friend WithEvents LearningRate_Trackbar As TrackBar
    Friend WithEvents LRValue_Label As Label
    Friend WithEvents Momentum_Label As Label
    Friend WithEvents Momentum_Trackbar As TrackBar
    Friend WithEvents MomentumValue_Label As Label
    Friend WithEvents Bias_Label As Label
    Friend WithEvents Bias_CheckBox As CheckBox
    Friend WithEvents TrainData_Label As Label
    Friend WithEvents TrainData_TextBox As TextBox
    Friend WithEvents TrainBrowse_Button As Button
    Friend WithEvents TestBrowse_Button As Button
    Friend WithEvents TestData_TextBox As TextBox
    Friend WithEvents TestData_Label As Label
    Friend WithEvents Finish_Button As Button
End Class
