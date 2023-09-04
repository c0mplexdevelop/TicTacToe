Option Strict On

Imports System.Text

Namespace TicTacToe

    Module Program

        Private Const EMPTY_SPACE As String = " "

        Private board(,) As String = New String(2, 2) {
            {EMPTY_SPACE, EMPTY_SPACE, EMPTY_SPACE},
            {EMPTY_SPACE, EMPTY_SPACE, EMPTY_SPACE},
            {EMPTY_SPACE, EMPTY_SPACE, EMPTY_SPACE}
        }
        Private currentTurn As String = "X"
        Sub Main(args As String())
            Dim winning As Boolean = False
            While Not winning
                printBoard()

                Console.Write($"{currentTurn}, Input a Position: ")
                Dim input As String = Console.ReadLine()

                If Not IsNumeric(input) Then
                    Console.WriteLine($"The input: ""{input}"", is not numeric!")
                    Continue While
                ElseIf Integer.Parse(input) < 1 Then
                    Console.WriteLine($"The input: ""{input}"", is below 1!")
                    Continue While
                ElseIf Integer.Parse(input) > 9 Then
                    Console.WriteLine($"The input: ""{input}"", is above 9!")
                    Continue While
                End If

                Dim intInput As Integer = Integer.Parse(input) - 1
                Dim row As Integer = intInput \ 3 ' \ is Integer division, / is floating point division
                Dim col As Integer = intInput Mod 3

                'Console.Write("Input a Col: ")
                'Dim colInput As String = Console.ReadLine()
                'If Not IsNumeric(colInput) Then
                '    Console.WriteLine("The input is not numeric!")
                '    Continue While
                'End If

                If Not checkIfEmpty(row, col) Then
                    Console.WriteLine($"The position is taken by {board(row, col)}!")
                    Continue While
                End If

                board(row, col) = currentTurn
                If checkIfSomeoneWon() Then
                    printBoard()
                    Console.WriteLine($"{currentTurn} Won!")
                    winning = True
                End If

                currentTurn = If(currentTurn = "X", "O", "X")

            End While
        End Sub

        Function checkIfEmpty(row As Integer, col As Integer) As Boolean
            Return board(row, col) = EMPTY_SPACE
        End Function

        Function checkIfSomeoneWon() As Boolean
            Return checkIfRowWon() OrElse checkIfColWon() OrElse checkIfRightDiagonalWon() OrElse checkIfLeftDiagonalWon()
        End Function

        Function checkIfRowWon() As Boolean
            For idx As Integer = 0 To board.GetLength(0) - 1
                If board(idx, 0) <> EMPTY_SPACE AndAlso board(idx, 0) = board(idx, 1) AndAlso board(idx, 1) = board(idx, 2) Then
                    ' <> is not equal to (!=)
                    ' AndAlso is different from And since And evaluates both,
                    ' but AndAlso evaluates the rightside if only the left side is true
                    Return True
                End If
            Next

            Return False
        End Function

        Function checkIfColWon() As Boolean
            For idx As Integer = 0 To board.GetLength(0) - 1
                If board(0, idx) <> EMPTY_SPACE AndAlso board(0, idx) = board(1, idx) AndAlso board(1, idx) = board(2, idx) Then
                    Return True
                End If
            Next

            Return False
        End Function

        Function checkIfLeftDiagonalWon() As Boolean
            If board(2, 2) <> EMPTY_SPACE AndAlso board(2, 2) = board(1, 1) AndAlso board(1, 1) = board(0, 0) Then
                Return True
            End If

            Return False
        End Function

        Function checkIfRightDiagonalWon() As Boolean
            If board(2, 0) <> EMPTY_SPACE AndAlso board(2, 0) = board(1, 1) AndAlso board(1, 1) = board(0, 2) Then
                Return True
            End If

            Return False
        End Function

        Sub printBoard()
            Dim boardBuilder As New StringBuilder()
            Dim boardSize As Integer = board.GetLength(0)

            For row As Integer = boardSize - 1 To 0 Step -1
                Dim stringRow As New StringBuilder()
                stringRow.Append(String.Format("{0}|{1}|{2}", board(row, 0), board(row, 1), board(row, 2))).Append(vbCrLf)
                boardBuilder.Append(stringRow.ToString())
            Next

            Console.WriteLine(boardBuilder.ToString())
            boardBuilder.Clear()


        End Sub

    End Module

End Namespace
