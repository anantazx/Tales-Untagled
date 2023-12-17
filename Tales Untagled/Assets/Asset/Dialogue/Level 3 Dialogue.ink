INCLUDE Globals.ink

{ActiveEndGame == "": -> main | -> alreadyChose}

=== main ===

Halo, apakah kamu <color=\#DB1D00>Samosir?</color> #speaker:<color=\#E17220>Adi</color> #potrait:Player #layout:left

Iya, tapi kamu siapa? #speaker:<color=\#DB1D00>Samosir</color> #potrait:Samosir #layout:right
*[Namaku]
    Namaku <color=\#E17220>Aditya</color>. #speaker:<color=\#E17220>Adi</color> #potrait:Player #layout:left
    Apa maksud kedatanganmu kesini ? #speaker:<color=\#DB1D00>Samosir</color> #potrait:Samosir #layout:right
    Aku disuruh ayahmu untuk memberitahukanmu agar kamu segera pulang.#speaker:<color=\#E17220>Adi</color> #potrait:Player #layout:left
    
*[Tidak]
    Kamu tidak perlu tau siapa aku.#speaker:<color=\#E17220>Adi</color> #potrait:Player #layout:left
    Lalu, kamu mau ngapain? #speaker:<color=\#DB1D00>Samosir</color> #potrait:Samosir #layout:right
    Ayahmu menyuruhmu pulang sekarang juga #speaker:<color=\#E17220>Adi</color> #potrait:Player #layout:left

- Ayah menyuruhku pulang? Tapi aku sedang asyik bermain di sini.#speaker:<color=\#DB1D00>Samosir</color> #potrait:Samosir #layout:right
Aku tidak ingin pulang! #speaker:<color=\#DB1D00>Samosir</color> #potrait:Samosir #layout:right
Kata ayahmu, di rumah sudah ada makanan yang menantimu #speaker:<color=\#E17220>Adi</color> #potrait:Player #layout:left
(Makanan? Aku lapar sekali.) #speaker:<color=\#DB1D00>Samosir</color> #potrait:Samosir #layout:right
Kalau begitu aku pulang, namun aku akan menyelesaikan permainanku terlebih dahulu. Apakah kamu juga akan ke rumah bersamaku? #speaker:<color=\#DB1D00>Samosir</color> #potrait:Samosir #layout:right
Tentu aku akan kerumahmu. #speaker:<color=\#E17220>Adi</color> #potrait:Player #layout:left
Baiklah jika begitu, aku akan menyusulmu #speaker:<color=\#DB1D00>Samosir</color> #potrait:Samosir #layout:right
~ ActiveEndGame = "Open"
->DONE

=== alreadyChose ===
aku pasti akan menyusulmu kawan, jangan khawatir. #speaker:<color=\#DB1D00>Samosir</color> #potrait:Samosir #layout:right
-> DONE



