import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-start-game',
  imports: [],
  templateUrl: './start-game.html',
  styleUrl: './start-game.css',
})
export class StartGame implements OnInit {

  prizes = [
    "A one way ticket to the moon!",
    "A life-sized cutout of your favorite celebrity!",
    "A year's supply of bubble wrap!",
    "A lifetime subscription to advertising brochures!",
  ]
  prize: string = "";
  constructor(private router: Router) {}

  ngOnInit(): void {
    this.selectPrize();
  }

  selectPrize() {
    this.prize = this.prizes[Math.floor(Math.random() * this.prizes.length)]
  }

  StartGame() {
    this.router.navigate(['/quiz']);
  }
}
