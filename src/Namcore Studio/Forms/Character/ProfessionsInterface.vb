﻿'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'* Copyright (C) 2013-2014 NamCore Studio <https://github.com/megasus/Namcore-Studio>
'*
'* This program is free software; you can redistribute it and/or modify it
'* under the terms of the GNU General Public License as published by the
'* Free Software Foundation; either version 3 of the License, or (at your
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
'*      /Filename:      ProfessionsInterface
'*      /Description:   Provides an interface to display character profession information
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Imports NCFramework.My
Imports NamCore_Studio.Modules.Interface
Imports NCFramework.Framework.Functions
Imports NCFramework.Framework.Logging
Imports NCFramework.Framework.Modules
Imports NamCore_Studio.Forms.Extension
Imports libnc.Provider

Namespace Forms.Character
    Public Class ProfessionsInterface
        Inherits EventTrigger

        '// Declaration
        Shared _activeProfession As Profession
        Shared _nylearnedSpellsLst As List(Of ProfessionSpell)
        Shared _maxProgressSize As UInteger
        Shared _loaded As Boolean = False
        Shared _lstitems As List(Of ListViewItem)
        Shared _temporarySkillLevel As Integer
        '// Declaration

        Public Sub PrepareInterface(ByVal setId As Integer)
            LogAppend("Loading player professions", "ProfessionsInterface_PrepareInterface", True)
            Hide()
            If GlobalVariables.currentEditedCharSet Is Nothing Then
                GlobalVariables.currentEditedCharSet = DeepCloneHelper.DeepClone(GlobalVariables.currentViewedCharSet)
            End If
            If GlobalVariables.currentEditedCharSet.Professions Is Nothing Then _
                GlobalVariables.currentEditedCharSet.Professions = New List(Of Profession)()
            If GlobalVariables.currentEditedCharSet.Spells Is Nothing Then _
                GlobalVariables.currentEditedCharSet.Spells = New List(Of Spell)()
            Dim firstResult As Boolean = True
            mainprof1_lbl.Text = "Add"
            mainprof1_pic.BackgroundImage = Nothing
            mainprof2_lbl.Text = "Add"
            mainprof2_pic.BackgroundImage = Nothing
            For Each prof As Profession In GlobalVariables.currentEditedCharSet.Professions
                If prof.Primary = True Then
                    If firstResult Then
                        mainprof1_select.Tag = prof
                        mainprof1_lbl.Text = prof.Name
                        mainprof1_pic.BackgroundImage = GetProfessionPic(prof.Id)
                        mainprof1_lbl.ForeColor = Color.White
                        _activeProfession = prof
                        rank_panel.Visible = True
                        firstResult = False
                    Else
                        mainprof2_select.Tag = prof
                        mainprof2_lbl.Text = prof.Name
                        mainprof2_pic.BackgroundImage = GetProfessionPic(prof.Id)
                    End If
                Else
                    Dim result As Profession =
                            GlobalVariables.currentEditedCharSet.Professions.Find(
                                Function(profession) profession.Primary = True)
                    Select Case prof.Id
                        Case 794
                            '// Archaeology
                            minprof1_select.Tag = prof
                            minprof1_lbl.Enabled = True
                            If result Is Nothing Then
                                If _activeProfession Is Nothing Then
                                    _activeProfession = prof
                                    minprof1_lbl.ForeColor = Color.White
                                    rank_panel.Visible = True
                                End If
                            End If
                        Case 185
                            '// Cooking
                            minprof2_select.Tag = prof
                            minprof2_lbl.Enabled = True
                            If result Is Nothing Then
                                If _activeProfession Is Nothing Then
                                    _activeProfession = prof
                                    minprof2_lbl.ForeColor = Color.White
                                    rank_panel.Visible = True
                                End If
                            End If
                        Case 129
                            '// First Aid
                            minprof3_select.Tag = prof
                            minprof3_lbl.Enabled = True
                            If result Is Nothing Then
                                If _activeProfession Is Nothing Then
                                    _activeProfession = prof
                                    minprof3_lbl.ForeColor = Color.White
                                    rank_panel.Visible = True
                                End If
                            End If
                        Case 356
                            '// Fishing
                            minprof4_select.Tag = prof
                            minprof4_lbl.Enabled = True
                            If result Is Nothing Then
                                If _activeProfession Is Nothing Then
                                    _activeProfession = prof
                                    minprof4_lbl.ForeColor = Color.White
                                    rank_panel.Visible = True
                                End If
                            End If
                    End Select
                End If
            Next
            mainprof1_lbl.Tag = mainprof1_select
            mainprof1_pic.Tag = mainprof1_select
            mainprof2_lbl.Tag = mainprof2_select
            mainprof2_pic.Tag = mainprof2_select
            minprof1_lbl.Tag = minprof1_select
            minprof1_pic.Tag = minprof1_select
            minprof2_lbl.Tag = minprof2_select
            minprof2_pic.Tag = minprof2_select
            minprof3_lbl.Tag = minprof3_select
            minprof3_pic.Tag = minprof3_select
            minprof4_lbl.Tag = minprof4_select
            minprof4_pic.Tag = minprof4_select
            If GlobalVariables.currentEditedCharSet.Professions.Count <> 0 Then
                ProfessionChange()
            End If
            _loaded = True
            Show()
        End Sub

        Private Function GetProfessionPic(ByVal professionId As Integer) As Image
            Select Case professionId
                Case 171 : Return My.Resources.trade_alchemy
                Case 164 : Return My.Resources.trade_blacksmithing
                Case 333 : Return My.Resources.trade_engraving
                Case 202 : Return My.Resources.trade_engineering
                Case 182 : Return My.Resources.spell_nature_naturetouchgrow
                Case 773 : Return My.Resources.inv_inscription_tradeskill01
                Case 755 : Return My.Resources.inv_misc_gem_01
                Case 165 : Return My.Resources.inv_misc_armorkit_17
                Case 186 : Return My.Resources.trade_mining
                Case 393 : Return My.Resources.inv_misc_pelt_wolf_01
                Case 197 : Return My.Resources.trade_tailoring
                Case Else : Return My.Resources.INV_Misc_QuestionMark
            End Select
        End Function

        Private Sub ProfessionInterface_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            AddHandler highlighter2.Click, AddressOf highlighter2_Click
            Dim controlLst As List(Of Control)
            controlLst = FindAllChildren()
            For Each itemControl As Control In controlLst
                itemControl.SetDoubleBuffered()
            Next
        End Sub

        Private Sub ProfessionChange()
            rank_panel.Visible = True
            _loaded = False
            _maxProgressSize = rank_panel.Size.Width
            rank_slider.Size = New Size(rank_panel.Size.Width + 10, rank_slider.Size.Height)
            _rank_color_panel.Size = New Size(_maxProgressSize/600*_activeProfession.Rank, rank_color_panel.Size.Height)
            rank_slider.Value = _activeProfession.Rank
            progress_lbl.Text = _activeProfession.Rank.ToString & "/600"
            rankname_lbl.Text = GetProficiencyLevelNameByLevel(_activeProfession.Rank)
            Dim relevantSpellList As List(Of ProfessionSpell) = ExecuteSkillLineSearch(_activeProfession.Id)
            _nylearnedSpellsLst = New List(Of ProfessionSpell)()
            For Each profSpell As ProfessionSpell In relevantSpellList
                Dim entry As ProfessionSpell =
                        _activeProfession.Recipes.Find(
                            Function(professionSpell) professionSpell.SpellId = profSpell.SpellId)
                If entry Is Nothing Then
                    _nylearnedSpellsLst.Add(profSpell)
                End If
            Next
            prof_lst.Items.Clear()
            nyl_bt.Enabled = True
            learned_bt.Enabled = False
            learned_bt.Text = "Learned (" & _activeProfession.Recipes.Count.ToString() & ")"
            nyl_bt.Text = "Not Yet Learned (" & _nylearnedSpellsLst.Count.ToString() & ")"
            For Each profSpell As ProfessionSpell In _activeProfession.Recipes
                If profSpell.Name Is Nothing Then _
                    profSpell.Name = GetSpellNameBySpellId(profSpell.SpellId, MySettings.Default.language)
                Dim str(2) As String
                str(0) = profSpell.SpellId.ToString()
                str(1) = profSpell.Name
                str(2) = profSpell.MinSkill.ToString()
                prof_lst.Items.Add(New ListViewItem(str) With {.Tag = profSpell})
            Next
            If _lstitems Is Nothing Then _lstitems = New List(Of ListViewItem)
            For Each itm As ListViewItem In prof_lst.Items
                Dim itmnew As ListViewItem = itm.Clone()
                _lstitems.Add(itmnew)
            Next
            _temporarySkillLevel = _activeProfession.Rank
            _loaded = True
            resultstatus_lbl.Text = prof_lst.Items.Count.ToString & " results!"
            LearnToolStrip.Text = "Unlearn"
        End Sub

        Private Function ExecuteSkillLineSearch(ByVal startvalue As Integer) As List(Of ProfessionSpell)
            Dim retnLst As New List(Of ProfessionSpell)
            Try
                Dim foundRows() As DataRow
                foundRows = GetSkillLineAbility().Select("SkillId = '" & startvalue.ToString() & "'")
                If foundRows.Length = 0 Then
                    Return retnLst
                Else
                    For z = 0 To foundRows.Length - 1
                        Dim profSpell As New ProfessionSpell
                        profSpell.SpellId = TryInt((foundRows(z)(1)))
                        profSpell.MinSkill = TryInt((foundRows(z)(2)))
                        profSpell.Name = GetSpellNameBySpellId(profSpell.SpellId, MySettings.Default.language)
                        retnLst.Add(profSpell)
                    Next
                    Return retnLst
                End If
            Catch ex As Exception
                Return retnLst
            End Try
        End Function

        Private Sub highlighter2_Click(sender As Object, e As EventArgs)
            Close()
        End Sub

        Private Sub prof_lst_MouseUp(sender As Object, e As MouseEventArgs) Handles prof_lst.MouseUp
            Userwait.Close()
            If e.Button = MouseButtons.Right Then
                If prof_lst.SelectedItems.Count = 0 Then Exit Sub
                profContext.Show(prof_lst, e.X, e.Y)
            End If
        End Sub

        Private Sub search_tb_Enter(sender As Object, e As EventArgs) Handles search_tb.Enter
            search_tb.Text = ""
        End Sub

        Private Sub search_tb_Leave(sender As Object, e As EventArgs) Handles search_tb.Leave
            If search_tb.Text = "" Then
                search_tb.Text = "Enter profession id"
            End If
        End Sub

        Private Sub search_tb_TextChanged(sender As Object, e As EventArgs) Handles search_tb.TextChanged
            If _loaded = False Then Exit Sub
            If search_tb.Text = "Enter profession id" Or search_tb.Text = "" Then
                If _lstitems Is Nothing Then Exit Sub
                If _lstitems.Count = 0 Then Exit Sub
                prof_lst.Items.Clear()
                For Each itm As ListViewItem In _lstitems
                    prof_lst.Items.Add(itm)
                Next
                prof_lst.Update()
                resultstatus_lbl.Text = prof_lst.Items.Count.ToString & " results!"
                Exit Sub
            End If
            Dim value As Integer = TryInt(search_tb.Text)
            Dim resultcounter As Integer = 0
            Dim itmstoshow As New List(Of ListViewItem)
            If Not value = 0 Then
                prof_lst.Items.Clear()
                For Each itm As ListViewItem In _lstitems
                    Dim profspell As ProfessionSpell = itm.Tag
                    If profspell.SpellId.ToString.Contains(value.ToString()) Then
                        resultcounter += 1
                        itmstoshow.Add(itm)
                    End If
                Next
                For Each profitm In itmstoshow
                    prof_lst.Items.Add(profitm)
                Next
                resultstatus_lbl.Text = resultcounter.ToString & " results!"
            Else
                prof_lst.Items.Clear()
                For Each itm As ListViewItem In _lstitems
                    prof_lst.Items.Add(itm)
                Next
                search_tb.Text = "Enter profession id"
            End If
            prof_lst.Update()
        End Sub

        Private Sub ProfessionClick(sender As Object, e As EventArgs) _
            Handles minprof4_select.Click, minprof4_pic.Click, minprof4_lbl.Click, minprof3_select.Click,
                    minprof3_pic.Click, minprof3_lbl.Click, minprof2_select.Click, minprof2_pic.Click,
                    minprof2_lbl.Click, minprof1_select.Click, minprof1_pic.Click, minprof1_lbl.Click,
                    mainprof2_select.Click, mainprof2_pic.Click, mainprof2_lbl.Click, mainprof1_select.Click,
                    mainprof1_pic.Click, mainprof1_lbl.Click
            Dim senderPanel As Panel
            If TypeOf (sender) Is PictureBox Or TypeOf (sender) Is Label Then
                senderPanel = sender.Tag
            Else
                senderPanel = sender
            End If
            Dim prof As Profession = senderPanel.Tag
            If prof Is Nothing Then
                If senderPanel.Name.StartsWith("minprof") Then
                    Dim msgResult As MsgBoxResult = MsgBox(ResourceHandler.GetUserMessage("learnprofession"),
                                                           MsgBoxStyle.YesNo, "Profession not known")
                    If msgResult = MsgBoxResult.Yes Then
                        Select Case senderPanel.Name
                            Case "minprof1_select"
                                Dim newProf As New Profession
                                newProf.Recipes = New List(Of ProfessionSpell)()
                                newProf.Primary = False
                                newProf.Id = 794
                                newProf.Rank = 1
                                newProf.Name = "Archaeology"
                                newProf.Max = 75
                                GlobalVariables.currentEditedCharSet.Professions.Add(newProf)
                                senderPanel.Tag = newProf
                                minprof1_lbl.Enabled = True
                            Case "minprof2_select"
                                Dim newProf As New Profession
                                newProf.Recipes = New List(Of ProfessionSpell)()
                                newProf.Primary = False
                                newProf.Id = 185
                                newProf.Rank = 1
                                newProf.Name = "Cooking"
                                newProf.Max = 75
                                GlobalVariables.currentEditedCharSet.Professions.Add(newProf)
                                senderPanel.Tag = newProf
                                minprof2_lbl.Enabled = True
                            Case "minprof3_select"
                                Dim newProf As New Profession
                                newProf.Recipes = New List(Of ProfessionSpell)()
                                newProf.Primary = False
                                newProf.Id = 129
                                newProf.Rank = 1
                                newProf.Name = "First Aid"
                                newProf.Max = 75
                                GlobalVariables.currentEditedCharSet.Professions.Add(newProf)
                                senderPanel.Tag = newProf
                                minprof3_lbl.Enabled = True
                            Case "minprof4_select"
                                Dim newProf As New Profession
                                newProf.Recipes = New List(Of ProfessionSpell)()
                                newProf.Primary = False
                                newProf.Id = 356
                                newProf.Rank = 1
                                newProf.Name = "Fishing"
                                newProf.Max = 75
                                GlobalVariables.currentEditedCharSet.Professions.Add(newProf)
                                senderPanel.Tag = newProf
                                minprof4_lbl.Enabled = True
                            Case Else
                                Exit Sub
                        End Select
                        prof = senderPanel.Tag
                        Dim spell2Add As Integer = GetSkillSpellIdBySkillRank(prof.Id, prof.Rank)
                        Dim specialSpells2Add() As Integer = GetSkillSpecialSpellIdBySkill(prof.Id)
                        If Not spell2Add = -1 And spell2Add <> 0 Then _
                            GlobalVariables.currentEditedCharSet.Spells.Add(
                                New Spell _
                                                                               With {.Active = 1, .Disabled = 0,
                                                                               .Id = spell2Add})
                        If Not specialSpells2Add Is Nothing Then
                            For i = 0 To specialSpells2Add.Length - 1
                                LogAppend("Adding special spell " & specialSpells2Add(i).ToString(),
                                          "ProfessionsInterface_ProfessionClick", False)
                                GlobalVariables.currentEditedCharSet.Spells.Add(
                                    New Spell _
                                                                                   With {.Active = 1, .Disabled = 0,
                                                                                   .Id = specialSpells2Add(i)})
                            Next
                        End If
                    Else
                        Exit Sub
                    End If
                Else

                End If
            End If
            _activeProfession = prof
            For Each actrl As Control In menupanel.Controls
                For Each ctrl As Control In actrl.Controls
                    If ctrl.Name.EndsWith("_lbl") Then
                        DirectCast(ctrl, Label).ForeColor = Color.Black
                        Dim thisPanel As Panel = ctrl.Tag
                        Dim thisProf As Profession = thisPanel.Tag
                        If thisProf IsNot Nothing Then
                            If thisProf.Id = prof.Id Then
                                DirectCast(ctrl, Label).ForeColor = Color.White
                            End If
                        End If
                    End If
                Next
            Next
            ProfessionChange()
        End Sub

        Private Sub HoverHandler_Enter(sender As Object, e As EventArgs) _
            Handles minprof4_select.MouseEnter, minprof3_select.MouseEnter, minprof2_select.MouseEnter,
                    minprof1_select.MouseEnter, mainprof2_select.MouseEnter, mainprof1_select.MouseEnter,
                    minprof4_pic.MouseEnter, minprof3_pic.MouseEnter, minprof2_pic.MouseEnter, minprof1_pic.MouseEnter,
                    mainprof2_pic.MouseEnter, mainprof1_pic.MouseEnter, mainprof1_lbl.MouseEnter,
                    minprof4_lbl.MouseEnter, minprof3_lbl.MouseEnter, minprof2_lbl.MouseEnter, minprof1_lbl.MouseEnter,
                    mainprof2_lbl.MouseEnter
            Dim senderPanel As Panel
            If TypeOf (sender) Is PictureBox Or TypeOf (sender) Is Label Then
                senderPanel = sender.Tag
            Else
                senderPanel = sender
            End If
            senderPanel.BackgroundImage = My.Resources.highlight
        End Sub

        Private Sub HoverHandler_Leave(sender As Object, e As EventArgs) _
            Handles minprof4_select.MouseLeave, minprof3_select.MouseLeave, minprof2_select.MouseLeave,
                    minprof1_select.MouseLeave, mainprof2_select.MouseLeave, mainprof1_select.MouseLeave,
                    minprof4_pic.MouseLeave, minprof3_pic.MouseLeave, minprof2_pic.MouseLeave, minprof1_pic.MouseLeave,
                    mainprof2_pic.MouseLeave, mainprof1_pic.MouseLeave, mainprof1_lbl.MouseLeave,
                    minprof4_lbl.MouseLeave, minprof3_lbl.MouseLeave, minprof2_lbl.MouseLeave, minprof1_lbl.MouseLeave,
                    mainprof2_lbl.MouseLeave
            Dim senderPanel As Panel
            If TypeOf (sender) Is PictureBox Or TypeOf (sender) Is Label Then
                senderPanel = sender.Tag
            Else
                senderPanel = sender
            End If
            senderPanel.BackgroundImage = Nothing
        End Sub

        Private Sub rank_slider_MouseDown(sender As Object, e As MouseEventArgs) Handles rank_slider.MouseDown
            _temporarySkillLevel = _activeProfession.Rank
        End Sub

        Private Sub rank_slider_MouseUp(sender As Object, e As MouseEventArgs) Handles rank_slider.MouseUp
            Dim spell2Remove As Integer = GetSkillSpellIdBySkillRank(_activeProfession.Id, _temporarySkillLevel)
            If Not spell2Remove = -1 And spell2Remove <> 0 Then
                Dim result As Spell =
                        GlobalVariables.currentEditedCharSet.Spells.Find(Function(spell) spell.Id = spell2Remove)
                If Not result Is Nothing Then GlobalVariables.currentEditedCharSet.Spells.Remove(result)
            End If
            Dim spell2Add As Integer = GetSkillSpellIdBySkillRank(_activeProfession.Id, _activeProfession.Rank)
            If Not spell2Add = -1 And spell2Add <> 0 Then _
                GlobalVariables.currentEditedCharSet.Spells.Add(
                    New Spell _
                                                                   With {.Active = 1, .Disabled = 0, .Id = spell2Add})
        End Sub

        Private Sub rank_slider_Scroll(sender As Object, e As EventArgs) Handles rank_slider.Scroll
            _activeProfession.Rank = rank_slider.Value
            rank_color_panel.Size = New Size(_maxProgressSize/600*_activeProfession.Rank, rank_color_panel.Size.Height)
            progress_lbl.Text = _activeProfession.Rank.ToString & "/600"
            rankname_lbl.Text = GetProficiencyLevelNameByLevel(_activeProfession.Rank)
        End Sub

        Private Sub learned_bt_Click(sender As Object, e As EventArgs) Handles learned_bt.Click
            prof_lst.Items.Clear()
            _lstitems.Clear()
            nyl_bt.Enabled = True
            learned_bt.Enabled = False
            For Each profSpell As ProfessionSpell In _activeProfession.Recipes
                Dim str(2) As String
                str(0) = profSpell.SpellId.ToString()
                str(1) = profSpell.Name
                str(2) = profSpell.MinSkill.ToString()
                prof_lst.Items.Add(New ListViewItem(str) With {.Tag = profSpell})
            Next
            If _lstitems Is Nothing Then _lstitems = New List(Of ListViewItem)
            For Each itm As ListViewItem In prof_lst.Items
                Dim itmnew As ListViewItem = itm.Clone()
                _lstitems.Add(itmnew)
            Next
            resultstatus_lbl.Text = prof_lst.Items.Count.ToString & " results!"
            LearnToolStrip.Text = "Unlearn"
            LearnAllToolStripMenuItem.Text = "Unlearn all"
        End Sub

        Private Sub nyl_bt_Click(sender As Object, e As EventArgs) Handles nyl_bt.Click
            prof_lst.Items.Clear()
            _lstitems.Clear()
            nyl_bt.Enabled = False
            learned_bt.Enabled = True
            For Each profSpell As ProfessionSpell In _nylearnedSpellsLst
                Dim str(2) As String
                str(0) = profSpell.SpellId.ToString()
                str(1) = profSpell.Name
                str(2) = profSpell.MinSkill.ToString()
                prof_lst.Items.Add(New ListViewItem(str) With {.Tag = profSpell})
            Next
            If _lstitems Is Nothing Then _lstitems = New List(Of ListViewItem)
            For Each itm As ListViewItem In prof_lst.Items
                Dim itmnew As ListViewItem = itm.Clone()
                _lstitems.Add(itmnew)
            Next
            resultstatus_lbl.Text = prof_lst.Items.Count.ToString & " results!"
            LearnToolStrip.Text = "Learn"
            LearnAllToolStripMenuItem.Text = "Learn all"
        End Sub

        Private Sub LearnToolStrip_Click(sender As Object, e As EventArgs) Handles LearnToolStrip.Click
            prof_lst.BeginUpdate()
            For Each listOb As ListViewItem In prof_lst.SelectedItems
                Dim senderTag As ProfessionSpell = listOb.Tag
                Select Case learned_bt.Enabled
                    Case True
                        '// Learn
                        _activeProfession.Recipes.Add(senderTag)
                        Dim result As ProfessionSpell =
                                _nylearnedSpellsLst.Find(
                                    Function(professionSpell) professionSpell.SpellId = senderTag.SpellId)
                        If result IsNot Nothing Then _nylearnedSpellsLst.Remove(result)
                    Case Else
                        '// Unlearn
                        _nylearnedSpellsLst.Add(senderTag)
                        Dim result As ProfessionSpell =
                                _activeProfession.Recipes.Find(
                                    Function(professionSpell) professionSpell.SpellId = senderTag.SpellId)
                        If result IsNot Nothing Then _activeProfession.Recipes.Remove(result)
                End Select
                learned_bt.Text = "Learned (" & _activeProfession.Recipes.Count.ToString() & ")"
                nyl_bt.Text = "Not Yet Learned (" & _nylearnedSpellsLst.Count.ToString() & ")"
                For Each pitm As ListViewItem In _lstitems
                    If pitm.Tag.SpellId = senderTag.SpellId Then
                        _lstitems.Remove(pitm)
                        Exit For
                    End If
                Next
                prof_lst.Items.Remove(listOb)
                resultstatus_lbl.Text = prof_lst.Items.Count.ToString & " results!"
            Next
            prof_lst.EndUpdate()
        End Sub

        Private Sub LearnAllToolStripMenuItem_Click(sender As Object, e As EventArgs) _
            Handles LearnAllToolStripMenuItem.Click
            prof_lst.BeginUpdate()
            For Each listOb As ListViewItem In prof_lst.Items
                Dim senderTag As ProfessionSpell = listOb.Tag
                Select Case learned_bt.Enabled
                    Case True
                        '// Learn
                        _activeProfession.Recipes.Add(senderTag)
                        Dim result As ProfessionSpell =
                                _nylearnedSpellsLst.Find(
                                    Function(professionSpell) professionSpell.SpellId = senderTag.SpellId)
                        If result IsNot Nothing Then _nylearnedSpellsLst.Remove(result)
                    Case Else
                        '// Unlearn
                        _nylearnedSpellsLst.Add(senderTag)
                        Dim result As ProfessionSpell =
                                _activeProfession.Recipes.Find(
                                    Function(professionSpell) professionSpell.SpellId = senderTag.SpellId)
                        If result IsNot Nothing Then _activeProfession.Recipes.Remove(result)
                End Select
                learned_bt.Text = "Learned (" & _activeProfession.Recipes.Count.ToString() & ")"
                nyl_bt.Text = "Not Yet Learned (" & _nylearnedSpellsLst.Count.ToString() & ")"
                For Each pitm As ListViewItem In _lstitems
                    If pitm.Tag.SpellId = senderTag.SpellId Then
                        _lstitems.Remove(pitm)
                        Exit For
                    End If
                Next
                prof_lst.Items.Remove(listOb)
                resultstatus_lbl.Text = prof_lst.Items.Count.ToString & " results!"
            Next
            prof_lst.EndUpdate()
        End Sub
    End Class
End Namespace