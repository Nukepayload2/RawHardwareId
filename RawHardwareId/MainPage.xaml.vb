' https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

Imports System.Globalization
Imports System.Text
Imports System.Text.RegularExpressions
''' <summary>
''' 可用于自身或导航至 Frame 内部的空白页。
''' </summary>
Public NotInheritable Class MainPage
    Inherits Page

    Private Model As New ConversionSpace

    Shared _getVen As New Regex("(?<=VEN_)[0-9A-F]{4}")
    Shared _getDev As New Regex("(?<=DEV_)[0-9A-F]{4}")
    Shared _getSubSys As New Regex("(?<=SUBSYS_)[0-9A-F]{8}")

    Private Sub BtnToHex_Click(sender As Object, e As RoutedEventArgs) Handles BtnToHex.Click
        Model.ErrorText = ""
        Dim srcText = Model.DeviceMgrFormat
        Try
            If String.IsNullOrEmpty(srcText) Then
                Throw New InvalidOperationException("请输入 设备 Id。")
            End If
            Dim match = Function(reg As Regex, errText$) As String
                            Dim value = reg.Match(srcText).Value
                            If String.IsNullOrEmpty(value) Then
                                Throw New InvalidOperationException(errText)
                            End If
                            Return value
                        End Function
            Dim ven = match(_getVen, "无法从 设备 Id 提取供应商代号。")
            Dim dev = match(_getDev, "无法从 设备 Id 提取制造商代号。")
            Dim subSys = match(_getSubSys, "无法从 设备 Id 提取子系统代号。")
            Dim hex As New StringBuilder
            With hex
                .Append(ven.Substring(2)).Append(ven.Substring(0, 2))
                .Append(dev.Substring(2)).Append(dev.Substring(0, 2))
                For i = 6 To 0 Step -2
                    .Append(subSys.Substring(i, 2))
                Next
            End With
            Model.RawFormat = hex.ToString
        Catch ex As Exception
            Model.ErrorText = ex.Message
            Model.RawFormat = ""
        End Try
    End Sub

    Private Sub BtnToDevId_Click(sender As Object, e As RoutedEventArgs) Handles BtnToDevId.Click
        Dim ulValue As ULong
        If ULong.TryParse(Model.RawFormat, NumberStyles.HexNumber, CultureInfo.InvariantCulture, ulValue) Then
            Using ms As New MemoryStream(BitConverter.GetBytes(ulValue).Reverse.ToArray),
                  sr As New BinaryReader(ms)
                Dim ven = sr.ReadUInt16.ToString("X4")
                Dim dev = sr.ReadUInt16.ToString("X4")
                Dim subSys = sr.ReadUInt32.ToString("X8")
                Model.DeviceMgrFormat = $"VEN_{ven}&DEV_{dev}&SUBSYS_{subSys}"
            End Using
        Else
            Model.DeviceMgrFormat = ""
            Model.ErrorText = "请检查输入的 Hex 格式数字。"
        End If
    End Sub

End Class
