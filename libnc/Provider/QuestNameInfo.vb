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
'*F:\Projekte\Visual Studio\Namcore-Studio\Namcore Studio Framework\Framework\Functions\SpellItemInformation.vb
'* Developed by Alcanmage/megasus
'*
'* //FileInfo//
'*      /Filename:      QuestNameInfo
'*      /Description:   Provides quest information
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports libnc.Main
Namespace Provider
    Public Module QuestNameInfo
        Public Function GetQuestTitleById(ByVal questId As Integer, ByVal locale As String) As String
            Dim targetField As Integer = 1
            If locale = "en" Then targetField += 1
            Dim myResult As String = ExecuteCsvSearch(QuestNameCsv, "Id", questId.ToString(), targetField)(0)
            If myResult = "-" Then myResult = "Not found"
            Return myResult
        End Function
    End Module
End Namespace