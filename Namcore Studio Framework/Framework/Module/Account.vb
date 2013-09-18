﻿'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013 Namcore Studio <https://github.com/megasus/Namcore-Studio>
'*
'* This program is free software; you can redistribute it and/or modify it
'* under the terms of the GNU General Public License as published by the
'* Free Software Foundation; either version 2 of the License, or (at your
'* option) any later version.
'*
'* This program is distributed in the hope that it will be useful, but WITHOUT
'* ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
'* FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for
'* more details.
'*
'* You should have received a copy of the GNU General Public License along
'* with this program. If not, see <http://www.gnu.org/licenses/>.
'*
'* Developed by Alcanmage/megasus
'*
'* //FileInfo//
'*      /Filename:      Account
'*      /Description:   Account Object - account information class
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Namespace Framework.Module
    <Serializable()>
    Public Class Account
        Public Id As Integer
        Public Name As String
        Public Transcharlist As ArrayList

        Public Sub New(accname As String, accountid As Integer)
            name = accname
            id = accountid
        End Sub
    End Class
End Namespace