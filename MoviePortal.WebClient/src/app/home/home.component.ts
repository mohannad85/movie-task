import { Component, OnInit } from '@angular/core';
import { Movie } from '../shared/models/movie';
import { MovieService } from '../shared/services/movies.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  movies!: Movie[];

  public homeText!: string;
  isWatchedActive!: boolean;
  isReminderActive!: boolean;

  constructor(private movieService: MovieService) { }

  ngOnInit(): void {
    this.movieService.getMovies()
      .subscribe(data => {
        this.movies = data;
        console.log(data);
      });
  }

  reminder() {
    this.isReminderActive = !this.isReminderActive;
  }

  watched() {
    this.isWatchedActive= !this.isWatchedActive;
  }

}
