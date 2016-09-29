<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEXTGEN
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.txtSite = New System.Windows.Forms.TextBox()
        Me.lblSite = New System.Windows.Forms.Label()
        Me.chkConnect = New System.Windows.Forms.CheckBox()
        Me.btnConnect = New System.Windows.Forms.Button()
        Me.lblDatabase = New System.Windows.Forms.Label()
        Me.txtDatabase = New System.Windows.Forms.TextBox()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.lblServer = New System.Windows.Forms.Label()
        Me.txtServerSpec = New System.Windows.Forms.TextBox()
        Me.lblUser = New System.Windows.Forms.Label()
        Me.txtUser = New System.Windows.Forms.TextBox()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.chkList = New System.Windows.Forms.CheckBox()
        Me.trSPs = New System.Windows.Forms.TreeView()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.flpInput = New System.Windows.Forms.FlowLayoutPanel()
        Me.chkInput = New System.Windows.Forms.CheckBox()
        Me.txtOutput = New System.Windows.Forms.TextBox()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtSite)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblSite)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkConnect)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnConnect)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDatabase)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDatabase)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtPassword)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblPassword)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblServer)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtServerSpec)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblUser)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtUser)
        Me.SplitContainer1.Panel1MinSize = 15
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(846, 464)
        Me.SplitContainer1.SplitterDistance = 225
        Me.SplitContainer1.TabIndex = 0
        '
        'txtSite
        '
        Me.txtSite.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSite.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.FormMetaDataExtractor.UI.My.MySettings.Default, "DBSite", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.txtSite.Location = New System.Drawing.Point(74, 125)
        Me.txtSite.Name = "txtSite"
        Me.txtSite.Size = New System.Drawing.Size(148, 20)
        Me.txtSite.TabIndex = 23
        Me.txtSite.Text = Global.FormMetaDataExtractor.UI.My.MySettings.Default.DBSite
        '
        'lblSite
        '
        Me.lblSite.AutoSize = True
        Me.lblSite.Location = New System.Drawing.Point(38, 129)
        Me.lblSite.Name = "lblSite"
        Me.lblSite.Size = New System.Drawing.Size(28, 13)
        Me.lblSite.TabIndex = 22
        Me.lblSite.Text = "Site:"
        '
        'chkConnect
        '
        Me.chkConnect.AutoSize = True
        Me.chkConnect.Checked = True
        Me.chkConnect.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkConnect.Dock = System.Windows.Forms.DockStyle.Top
        Me.chkConnect.Location = New System.Drawing.Point(0, 0)
        Me.chkConnect.Name = "chkConnect"
        Me.chkConnect.Size = New System.Drawing.Size(225, 17)
        Me.chkConnect.TabIndex = 21
        Me.chkConnect.Text = "Expand"
        Me.chkConnect.UseVisualStyleBackColor = True
        '
        'btnConnect
        '
        Me.btnConnect.Location = New System.Drawing.Point(17, 156)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(75, 23)
        Me.btnConnect.TabIndex = 20
        Me.btnConnect.Text = "Connect"
        Me.btnConnect.UseVisualStyleBackColor = True
        '
        'lblDatabase
        '
        Me.lblDatabase.AutoSize = True
        Me.lblDatabase.Location = New System.Drawing.Point(14, 52)
        Me.lblDatabase.Name = "lblDatabase"
        Me.lblDatabase.Size = New System.Drawing.Size(56, 13)
        Me.lblDatabase.TabIndex = 19
        Me.lblDatabase.Text = "Database:"
        '
        'txtDatabase
        '
        Me.txtDatabase.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDatabase.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.FormMetaDataExtractor.UI.My.MySettings.Default, "DBAppName", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.txtDatabase.Location = New System.Drawing.Point(73, 49)
        Me.txtDatabase.Name = "txtDatabase"
        Me.txtDatabase.Size = New System.Drawing.Size(149, 20)
        Me.txtDatabase.TabIndex = 18
        Me.txtDatabase.Text = Global.FormMetaDataExtractor.UI.My.MySettings.Default.DBAppName
        '
        'txtPassword
        '
        Me.txtPassword.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPassword.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.FormMetaDataExtractor.UI.My.MySettings.Default, "DBPassword", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.txtPassword.Location = New System.Drawing.Point(74, 99)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Size = New System.Drawing.Size(148, 20)
        Me.txtPassword.TabIndex = 15
        Me.txtPassword.Text = Global.FormMetaDataExtractor.UI.My.MySettings.Default.DBPassword
        Me.txtPassword.UseSystemPasswordChar = True
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Location = New System.Drawing.Point(14, 102)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(56, 13)
        Me.lblPassword.TabIndex = 14
        Me.lblPassword.Text = "Password:"
        '
        'lblServer
        '
        Me.lblServer.AutoSize = True
        Me.lblServer.Location = New System.Drawing.Point(29, 27)
        Me.lblServer.Name = "lblServer"
        Me.lblServer.Size = New System.Drawing.Size(41, 13)
        Me.lblServer.TabIndex = 13
        Me.lblServer.Text = "Server:"
        '
        'txtServerSpec
        '
        Me.txtServerSpec.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtServerSpec.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.FormMetaDataExtractor.UI.My.MySettings.Default, "DBServer", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.txtServerSpec.Location = New System.Drawing.Point(73, 24)
        Me.txtServerSpec.Name = "txtServerSpec"
        Me.txtServerSpec.Size = New System.Drawing.Size(149, 20)
        Me.txtServerSpec.TabIndex = 12
        Me.txtServerSpec.Text = Global.FormMetaDataExtractor.UI.My.MySettings.Default.DBServer
        '
        'lblUser
        '
        Me.lblUser.AutoSize = True
        Me.lblUser.Location = New System.Drawing.Point(38, 77)
        Me.lblUser.Name = "lblUser"
        Me.lblUser.Size = New System.Drawing.Size(32, 13)
        Me.lblUser.TabIndex = 11
        Me.lblUser.Text = "User:"
        '
        'txtUser
        '
        Me.txtUser.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtUser.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.FormMetaDataExtractor.UI.My.MySettings.Default, "DBUser", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.txtUser.Location = New System.Drawing.Point(74, 74)
        Me.txtUser.Name = "txtUser"
        Me.txtUser.Size = New System.Drawing.Size(148, 20)
        Me.txtUser.TabIndex = 10
        Me.txtUser.Text = Global.FormMetaDataExtractor.UI.My.MySettings.Default.DBUser
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.chkList)
        Me.SplitContainer2.Panel1.Controls.Add(Me.trSPs)
        Me.SplitContainer2.Panel1MinSize = 15
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer2.Size = New System.Drawing.Size(617, 464)
        Me.SplitContainer2.SplitterDistance = 205
        Me.SplitContainer2.TabIndex = 0
        '
        'chkList
        '
        Me.chkList.AutoSize = True
        Me.chkList.Dock = System.Windows.Forms.DockStyle.Top
        Me.chkList.Location = New System.Drawing.Point(0, 0)
        Me.chkList.Name = "chkList"
        Me.chkList.Size = New System.Drawing.Size(205, 17)
        Me.chkList.TabIndex = 1
        Me.chkList.Text = "Expand"
        Me.chkList.UseVisualStyleBackColor = True
        '
        'trSPs
        '
        Me.trSPs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.trSPs.Location = New System.Drawing.Point(0, 23)
        Me.trSPs.Name = "trSPs"
        Me.trSPs.Size = New System.Drawing.Size(205, 441)
        Me.trSPs.TabIndex = 0
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.flpInput)
        Me.SplitContainer3.Panel1.Controls.Add(Me.chkInput)
        Me.SplitContainer3.Panel1MinSize = 15
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.txtOutput)
        Me.SplitContainer3.Size = New System.Drawing.Size(408, 464)
        Me.SplitContainer3.SplitterDistance = 136
        Me.SplitContainer3.TabIndex = 0
        '
        'flpInput
        '
        Me.flpInput.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.flpInput.AutoScroll = True
        Me.flpInput.Location = New System.Drawing.Point(4, 23)
        Me.flpInput.Name = "flpInput"
        Me.flpInput.Size = New System.Drawing.Size(132, 441)
        Me.flpInput.TabIndex = 1
        '
        'chkInput
        '
        Me.chkInput.AutoSize = True
        Me.chkInput.Dock = System.Windows.Forms.DockStyle.Top
        Me.chkInput.Location = New System.Drawing.Point(0, 0)
        Me.chkInput.Name = "chkInput"
        Me.chkInput.Size = New System.Drawing.Size(136, 17)
        Me.chkInput.TabIndex = 0
        Me.chkInput.Text = "Expand"
        Me.chkInput.UseVisualStyleBackColor = True
        '
        'txtOutput
        '
        Me.txtOutput.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtOutput.Location = New System.Drawing.Point(0, 0)
        Me.txtOutput.Multiline = True
        Me.txtOutput.Name = "txtOutput"
        Me.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtOutput.Size = New System.Drawing.Size(268, 464)
        Me.txtOutput.TabIndex = 2
        '
        'frmEXTGEN
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(846, 464)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmEXTGEN"
        Me.Text = "frmEXTGEN"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.Panel2.PerformLayout()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents lblDatabase As Label
    Friend WithEvents txtDatabase As TextBox
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents lblPassword As Label
    Friend WithEvents lblServer As Label
    Friend WithEvents txtServerSpec As TextBox
    Friend WithEvents lblUser As Label
    Friend WithEvents txtUser As TextBox
    Friend WithEvents btnConnect As Button
    Friend WithEvents chkConnect As CheckBox
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents chkList As CheckBox
    Friend WithEvents trSPs As TreeView
    Friend WithEvents SplitContainer3 As SplitContainer
    Friend WithEvents chkInput As CheckBox
    Friend WithEvents txtOutput As TextBox
    Friend WithEvents flpInput As FlowLayoutPanel
    Friend WithEvents txtSite As TextBox
    Friend WithEvents lblSite As Label
End Class
