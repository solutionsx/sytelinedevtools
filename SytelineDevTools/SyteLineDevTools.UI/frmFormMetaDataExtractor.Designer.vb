<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSyteLineDevTools
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
        Me.lblUser = New System.Windows.Forms.Label()
        Me.lblServer = New System.Windows.Forms.Label()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.lblConfig = New System.Windows.Forms.Label()
        Me.lblOutputPath = New System.Windows.Forms.Label()
        Me.btnVendor = New System.Windows.Forms.Button()
        Me.btnNonVendor = New System.Windows.Forms.Button()
        Me.txtOutputPath = New System.Windows.Forms.TextBox()
        Me.txtConfig = New System.Windows.Forms.TextBox()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.txtServerSpec = New System.Windows.Forms.TextBox()
        Me.txtUser = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout
        '
        'lblUser
        '
        Me.lblUser.AutoSize = true
        Me.lblUser.Location = New System.Drawing.Point(41, 60)
        Me.lblUser.Name = "lblUser"
        Me.lblUser.Size = New System.Drawing.Size(32, 13)
        Me.lblUser.TabIndex = 1
        Me.lblUser.Text = "User:"
        '
        'lblServer
        '
        Me.lblServer.AutoSize = true
        Me.lblServer.Location = New System.Drawing.Point(32, 10)
        Me.lblServer.Name = "lblServer"
        Me.lblServer.Size = New System.Drawing.Size(41, 13)
        Me.lblServer.TabIndex = 3
        Me.lblServer.Text = "Server:"
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = true
        Me.lblPassword.Location = New System.Drawing.Point(17, 85)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(56, 13)
        Me.lblPassword.TabIndex = 4
        Me.lblPassword.Text = "Password:"
        '
        'lblConfig
        '
        Me.lblConfig.AutoSize = true
        Me.lblConfig.Location = New System.Drawing.Point(33, 110)
        Me.lblConfig.Name = "lblConfig"
        Me.lblConfig.Size = New System.Drawing.Size(40, 13)
        Me.lblConfig.TabIndex = 7
        Me.lblConfig.Text = "Config:"
        '
        'lblOutputPath
        '
        Me.lblOutputPath.AutoSize = true
        Me.lblOutputPath.Location = New System.Drawing.Point(6, 35)
        Me.lblOutputPath.Name = "lblOutputPath"
        Me.lblOutputPath.Size = New System.Drawing.Size(67, 13)
        Me.lblOutputPath.TabIndex = 9
        Me.lblOutputPath.Text = "Output Path:"
        '
        'btnVendor
        '
        Me.btnVendor.Location = New System.Drawing.Point(20, 166)
        Me.btnVendor.Name = "btnVendor"
        Me.btnVendor.Size = New System.Drawing.Size(100, 23)
        Me.btnVendor.TabIndex = 10
        Me.btnVendor.Text = "Extract Vendor"
        Me.btnVendor.UseVisualStyleBackColor = true
        '
        'btnNonVendor
        '
        Me.btnNonVendor.Location = New System.Drawing.Point(20, 207)
        Me.btnNonVendor.Name = "btnNonVendor"
        Me.btnNonVendor.Size = New System.Drawing.Size(100, 23)
        Me.btnNonVendor.TabIndex = 11
        Me.btnNonVendor.Text = "Extract Site"
        Me.btnNonVendor.UseVisualStyleBackColor = true
        '
        'txtOutputPath
        '
        Me.txtOutputPath.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.SyteLineDevTools.UI.My.MySettings.Default, "OutputPath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.txtOutputPath.Location = New System.Drawing.Point(76, 32)
        Me.txtOutputPath.Name = "txtOutputPath"
        Me.txtOutputPath.Size = New System.Drawing.Size(364, 20)
        Me.txtOutputPath.TabIndex = 8
        Me.txtOutputPath.Text = Global.SyteLineDevTools.UI.My.MySettings.Default.OutputPath
        '
        'txtConfig
        '
        Me.txtConfig.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.SyteLineDevTools.UI.My.MySettings.Default, "Config", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.txtConfig.Location = New System.Drawing.Point(77, 107)
        Me.txtConfig.Name = "txtConfig"
        Me.txtConfig.Size = New System.Drawing.Size(100, 20)
        Me.txtConfig.TabIndex = 6
        Me.txtConfig.Text = Global.SyteLineDevTools.UI.My.MySettings.Default.Config
        '
        'txtPassword
        '
        Me.txtPassword.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.SyteLineDevTools.UI.My.MySettings.Default, "Password", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.txtPassword.Location = New System.Drawing.Point(77, 82)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Size = New System.Drawing.Size(100, 20)
        Me.txtPassword.TabIndex = 5
        Me.txtPassword.Text = Global.SyteLineDevTools.UI.My.MySettings.Default.Password
        Me.txtPassword.UseSystemPasswordChar = true
        '
        'txtServerSpec
        '
        Me.txtServerSpec.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.SyteLineDevTools.UI.My.MySettings.Default, "Server", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.txtServerSpec.Location = New System.Drawing.Point(76, 7)
        Me.txtServerSpec.Name = "txtServerSpec"
        Me.txtServerSpec.Size = New System.Drawing.Size(364, 20)
        Me.txtServerSpec.TabIndex = 2
        Me.txtServerSpec.Text = Global.SyteLineDevTools.UI.My.MySettings.Default.Server
        '
        'txtUser
        '
        Me.txtUser.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.SyteLineDevTools.UI.My.MySettings.Default, "User", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.txtUser.Location = New System.Drawing.Point(77, 57)
        Me.txtUser.Name = "txtUser"
        Me.txtUser.Size = New System.Drawing.Size(100, 20)
        Me.txtUser.TabIndex = 0
        Me.txtUser.Text = Global.SyteLineDevTools.UI.My.MySettings.Default.User
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(155, 207)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(100, 23)
        Me.Button1.TabIndex = 12
        Me.Button1.Text = "Push Site"
        Me.Button1.UseVisualStyleBackColor = true
        '
        'frmSyteLineDevTools
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(452, 321)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnNonVendor)
        Me.Controls.Add(Me.btnVendor)
        Me.Controls.Add(Me.lblOutputPath)
        Me.Controls.Add(Me.txtOutputPath)
        Me.Controls.Add(Me.lblConfig)
        Me.Controls.Add(Me.txtConfig)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.lblPassword)
        Me.Controls.Add(Me.lblServer)
        Me.Controls.Add(Me.txtServerSpec)
        Me.Controls.Add(Me.lblUser)
        Me.Controls.Add(Me.txtUser)
        Me.Name = "frmSyteLineDevTools"
        Me.ShowIcon = false
        Me.Text = "Form Scripting"
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents txtUser As TextBox
    Friend WithEvents lblUser As Label
    Friend WithEvents lblServer As Label
    Friend WithEvents txtServerSpec As TextBox
    Friend WithEvents lblPassword As Label
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents lblConfig As Label
    Friend WithEvents txtConfig As TextBox
    Friend WithEvents lblOutputPath As Label
    Friend WithEvents txtOutputPath As TextBox
    Friend WithEvents btnVendor As Button
    Friend WithEvents btnNonVendor As Button
    Friend WithEvents Button1 As Button
End Class
