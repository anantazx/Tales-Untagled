INCLUDE Globals.ink

{choiceMake == "": -> main |-> alreadyChose}

-> main

=== main ===
hallo perkenalkan nama aku Toba #speaker:<color=\#228B22>ToadKing</color> #potrait:ToadKing_Talking #layout:right
siapakah nama kamu <color=\#FFC0CB>ganteng</color> 
    *Perkenalkan Nama aku Adi #speaker:Adi #potrait:Player_Calm#layout:left
        Salam Kenal ya agus, senang bertemu dengan kamu. #speaker:<color=\#228B22>ToadKing</color> #potrait:ToadKing_Amazed #layout:right
    *Tidak Perlu Tau Namaku Siapa ! #speaker:Adi #potrait:Player_Calm#layout:left
    okay jika begitu, tidak perlu marah seperti itu kawan.  #speaker:<color=\#228B22>ToadKing</color> #potrait:ToadKing_Angry  #layout:right
    - oiyaa aku ada rencana nih untuk meninggalkan tempat ini#speaker:<color=\#228B22>ToadKing</color> #potrait:ToadKing_Talking #layout:right
    yuk berkelana bersama dengan saya mencari ciwi ciwi cantik dan sexy 
    (ada yang aneh dengan orang ini seperti pernah melihatnya dalam suatu buku ??) #speaker:Adi #potrait:Player_Calm# #layout:left
    (hmmm apakah sebaaiknya aku bantu saja dia ya ??)#speaker:Adi #potrait:Player_Calm#layout:left
    +[Hayukkk] 
        -> chosen("hayukk")
        yukk kita mencari cewe cewe sexy dan cantikk cantikk
    +[Tidak Mau]
        maaf aku tidak ikut dahulu lagi malas sekali
        -> chosen("Tidak mau")
     
=== chosen(pilihan) ===
~ choiceMake = pilihan
{
    - pilihan == "hayukk":
        lets gooooooo !!!!#speaker:<color=\#228B22>ToadKing</color> #potrait:ToadKing_Confused #layout:right
    
    - pilihan == "Tidak mau":
        cari aku jika kamu berubah pikiran ya #speaker:<color=\#228B22>ToadKing</color> #potrait:ToadKing_Confused #layout:right
}
-> END

=== alreadyChose ===
{
    - choiceMake == "hayukk":
        tunggu apa lagi ayuk jalan#speaker:<color=\#228B22>ToadKing</color> #potrait:ToadKing_Talking #layout:right
    - choiceMake == "Tidak mau":
        apakah kamu berubah pikiran ganteng ?? #speaker:<color=\#228B22>ToadKing</color> #potrait:ToadKing_Talking #layout:right
}
->END