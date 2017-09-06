Public Class ConversionSpace
    Implements INotifyPropertyChanged

    Dim _DeviceMgrFormat As String
    Public Property DeviceMgrFormat As String
        Get
            Return _DeviceMgrFormat
        End Get
        Set(value As String)
            _DeviceMgrFormat = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(DeviceMgrFormat)))
        End Set
    End Property

    Dim _RawFormat As String
    Public Property RawFormat As String
        Get
            Return _RawFormat
        End Get
        Set(value As String)
            _RawFormat = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(RawFormat)))
        End Set
    End Property

    Dim _ErrorText As String
    Public Property ErrorText As String
        Get
            Return _ErrorText
        End Get
        Set(value As String)
            _ErrorText = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(ErrorText)))
        End Set
    End Property

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
End Class
