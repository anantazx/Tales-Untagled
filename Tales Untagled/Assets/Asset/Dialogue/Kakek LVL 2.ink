INCLUDE Globals.ink

{ActiveEndGame == "": -> main | -> alreadyChose}

=== main ===

haii anak muda, mau kah kau mendengarkan cerita ku ??#speaker:<color=\#687044>Kakek</color> #potrait:Kakek #layout:right

*[Baik]
    baiklah kakek, aku senang mendengarkan mu bercerita <color=\#687044>Kakek</color>. #speaker:<color=\#E17220>Adi</color> #potrait:Player #layout:left
    
    tahukah kau mengenai pria yang bernama <color=\#88BDFF>Toba</color> ?#speaker:<color=\#687044>Kakek</color> #potrait:Kakek #layout:right
    
    tahu <color=\#687044>kek</color>, ada apa ya ?? #speaker:<color=\#E17220>Adi</color> #potrait:Player #layout:left
    
    <color=\#88BDFF>Toba</color> merupakan seorang pria yang rajin dan bekerja keras#speaker:<color=\#687044>Kakek</color> #potrait:Kakek #layout:right
    
    sehari hari dia dengan giat bercocok tanam, dan menghabiskan harinya memancing#speaker:<color=\#687044>Kakek</color> #potrait:Kakek #layout:right
    
    namun suatu hari ia mendapatkan se ekor Ikan emas#speaker:<color=\#687044>Kakek</color> #potrait:Kakek #layout:right
    
    kemudian ikan mas tersebut menjadi seorang Wanita cantik, dan sekarang menjadi istrinya <color=\#88BDFF>Toba</color>.#speaker:<color=\#687044>Kakek</color> #potrait:Kakek #layout:right
    
    wow, kakek ini merupakan cerita yang aneh. #speaker:<color=\#E17220>Adi</color> #potrait:Player #layout:left
    ~ ActiveEndGame = "Open"
    -> END
    
*[Dasar]

    Dasar kau pria tua yang banyak omong. #speaker:<color=\#E17220>Adi</color> #potrait:Player #layout:left
    
    aku tidak ada waktu untukmu, silahkan berbicara kepada orang lain  #speaker:<color=\#E17220>Adi</color> #potrait:Player #layout:left
    
    Dasar <color=\#687044>Kakek</color> <color=\#687044>Kakek</color> Gilaa !!  #speaker:<color=\#E17220>Adi</color> #potrait:Player #layout:left
    
    Awas Kau Anak Muda, Dasar anak kualat.##speaker:<color=\#687044>Kakek</color> #potrait:Kakek #layout:right
    
    ~ ActiveEndGame = "Close"
    -> END
    
=== alreadyChose === 

ZZZZZZ..... #speaker:<color=\#687044>Kakek</color> #potrait:Kakek #layout:right

->END